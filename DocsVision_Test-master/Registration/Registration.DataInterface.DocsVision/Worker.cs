using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsVision.Platform.ObjectManager;

namespace Registration.DataInterface.DocsVision
{
    public class Worker : IWorker
    {
        private const string WorkerLogin = "Login";
        private const string WorkerName = "Name";
        private const string WorkerPassword = "Password";

        private readonly RowData _rowData;
        private RowData RowData
        {
            get { return _rowData; }
        }

        public Worker(RowData rowData)
        {
            _rowData = rowData;
        }

        public string Name
        {
            get { return RowData.GetString(WorkerName); }
            set { RowData.SetString(WorkerName, value); }
        }

        public string Login
        {
            get { return RowData.GetString(WorkerLogin); }
            set { RowData.SetString(WorkerLogin, value); }
        }

        public string Password
        {
            get { return RowData.GetString(WorkerPassword); }
            set { RowData.SetString(WorkerPassword, value); }
        }

        public Guid Id
        {
            get { return RowData.Id; }
        }

        public override string ToString()
        {
            return string.Format($"{Name} ({Login})");
        }

    }
}
