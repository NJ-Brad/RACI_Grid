using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RACI
{
    public partial class FileHandler : Component
    {
        public FileHandler()
        {
            InitializeComponent();
        }

        public FileHandler(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public Type? DataType { get; set; } = null;
        public string Name { get; set; } = "File";
        public string Extension { get; set; } = "data";
        public string FileTypeName { get; set; } = "Data File";
        public bool IncludeAllFileTypes { get; set; } = true;
        public object? Data { get; set; } = null;
        public string FileName { get; set; }= string.Empty;

        public bool Save()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                SaveAs();
                return true;
            }
            Type outputType = Data.GetType();

            File.WriteAllText(FileName, JsonSerializer.Serialize(Data, outputType, new JsonSerializerOptions { WriteIndented = true }));

            return true;
        }

        public void SaveAs()
        {
            SaveFileDialog sfd = new()
            {
                FileName = string.IsNullOrEmpty(FileName) ? Name : FileName,
                AddToRecent = true,
                AddExtension = true,
                DefaultExt = Extension,
                Filter = $"{FileTypeName}s (*.{Extension})|*.{Extension}"
            };
            if (IncludeAllFileTypes)
                sfd.Filter += "|All Files (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Type outputType = Data.GetType();

                //File.WriteAllText(sfd.FileName, JsonSerializer.Serialize<PlanDocument>(Document, new JsonSerializerOptions { WriteIndented = true }));
                File.WriteAllText(sfd.FileName, JsonSerializer.Serialize(Data, outputType, new JsonSerializerOptions { WriteIndented = true }));
                FileName = sfd.FileName;
            }
        }

        public bool Load()
        {
            bool rtnVal = false;
            OpenFileDialog ofd = new()
            {
                Multiselect = false,
                Filter = $"{FileTypeName}s (*.{Extension})|*.{Extension}"
            };
            if (IncludeAllFileTypes)
                ofd.Filter += "|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileText = File.ReadAllText(ofd.FileName);
                if(DataType == null)
                {
                    if(Data != null)
                    {
                        DataType = Data.GetType();
                    }
                }
                //Data = JsonSerializer.Deserialize(fileText, Data.GetType());
                Data = JsonSerializer.Deserialize(fileText, DataType);
                FileName = ofd.FileName;
                rtnVal = true;
            }
            return rtnVal;
        }
    }
}
