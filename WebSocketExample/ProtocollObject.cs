﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketExample
{
    public class ProtocollObject
    {
        /// <summary>
        /// Will be generated by the server and used to build unique web requests for the server.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// This adapter will be defined by the client and used by the server to generate the propper html and get/set requests.
        /// </summary>
        public string Adapter { get; set; }

        public string Name { get; set; }

        public List<ParamObject> ParamObjects { get; set; }

        public ProtocollObject(string message)
        {
            this.Identifier = StringManipulation.GetIdentifier(message);
            this.Adapter = StringManipulation.GetAdapter(message);
            this.Name = StringManipulation.GetName(message);
            this.ParamObjects = new List<ParamObject>();
            var possibleParameters = StringManipulation.Partioning(message, false);
            if (possibleParameters != null)
            {
                foreach (var possibleParam in possibleParameters)
                {
                    this.ParamObjects.Add(new ParamObject(possibleParam));
                }
            }
        }

        public string BuildProtocollMessage()
        {
            string parameters = $"identifier:{this.Identifier};adapter:{this.Adapter};name:{this.Name};;";

            foreach (var param in this.ParamObjects)
            {
                parameters += $"{param.ParamName}:{param.Value};";
            }

            return parameters;
        }
    }
}
