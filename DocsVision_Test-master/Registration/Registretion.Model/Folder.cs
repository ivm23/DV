using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Registration.Model
{
    public class Folder
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public Guid OwnerId { get; set; }
        public string Data { get; set; }

        public bool Empty()
        {
            return Id == Guid.Empty;
        }
    }
    public class FolderType
    {
        public int Id { get; set; }
        public string TypeClientUI { get; set; }
        public string TypeFolderService { get; set; }
        public string Name { get; set; }
    }
}