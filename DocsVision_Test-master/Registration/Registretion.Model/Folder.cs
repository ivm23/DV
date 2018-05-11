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

        public override bool Equals(object obj)
        {
            if (null == obj)
                return false;
            Folder folder = (Folder)obj;
            if (Id == folder.Id && ParentId == folder.ParentId && Name.Equals(folder.Name) && Type == folder.Type && OwnerId == folder.OwnerId && Data.Equals(folder.Data))
                return true;

            return false;
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