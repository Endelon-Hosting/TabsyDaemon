using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System;

namespace TabsyDaemon.Docker
{
public class DockerImage
{
        public long Containers { get; set; }
        public DateTime Created { get; set; }
        public string ID { get; set; }
        public IDictionary<string, string> Labels { get; set; }
        public string ParentID { get; set; }
        public IList<string> RepoDigests { get; set; }
        public IList<string> RepoTags { get; set; }
        public long SharedSize { get; set; }
        public long Size { get; set; }
        public long VirtualSize { get; set; }
        public string Name 
        { 
            get
            {
                return RepoTags[0];
            } 
        }
    }
}
