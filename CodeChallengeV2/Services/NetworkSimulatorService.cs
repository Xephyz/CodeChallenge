using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallengeV2.Models;

namespace CodeChallengeV2.Services
{
    public class NetworkSimulatorService : INetworkSimulatorService
    {
        /// <summary>
        /// Returns all nodes of type Gateway that if removed together with their connected edges would leave nodes of type Device, without edges to any Gateway. 
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public Task<List<Node>> FindCritialGateways(NetworkGraph graph)
        {
            var output = new List<Node>();
            var devices = graph.Graphs[0].Nodes.Where(x => x.Type == "Device");
            var gateways = graph.Graphs[0].Nodes.Where(x => x.Type == "Gateway");
            foreach (var node in devices)
            {
                var edges = graph.Graphs[0].Edges.Where(x => x.Source == node.Id);
                if (edges.Count() == 1)
                {
                    var edge = edges.FirstOrDefault();
                    var gateway = gateways.Where(x => x.Id == edge.Target).FirstOrDefault();
                    if (!output.Contains(gateway))
                    {
                        output.Add(gateway);
                    }
                }
            }
            return Task.FromResult(output);
        }
    }
}
