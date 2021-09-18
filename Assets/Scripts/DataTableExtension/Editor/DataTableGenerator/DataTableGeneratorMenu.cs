﻿using System.IO;
using System.Linq;
using GameFramework;
using UnityEditor;
using UnityEngine;

namespace DE.Editor.DataTableTools
{
    public sealed class DataTableGeneratorMenu
    {

        [MenuItem("DataTable/Generate DataTables From Txt")]
        public static void GenerateDataTables()
        {
            ExtensionsGenerate.GenerateExtensionByAnalysis(ExtensionsGenerate.DataTableType.Txt,2);
            foreach (var dataTableName in DataTableConfig.DataTableNames)
            {
                var dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }
        [MenuItem("DataTable/Generate DataTables From Excel")]
        public static void GenerateDataTablesFormExcel()
        {
            ExtensionsGenerate.GenerateExtensionByAnalysis(ExtensionsGenerate.DataTableType.Excel,2);

            foreach (var dataTableName in DataTableConfig.DataTableNames)
            {
                var dataTableProcessor = DataTableGenerator.CreateExcelDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }
    }
}