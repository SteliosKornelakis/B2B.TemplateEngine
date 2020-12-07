using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Spire.Doc;
using TemplateEngine.Docx;
using Document = Spire.Doc.Document;

namespace B2B.TemplateEngine
{
  public class Worker
  {
    private readonly string _inputPath;
    private readonly string _outputPath;


    private readonly List<ListMapper> _tableMapperList = new List<ListMapper>();

    private Dictionary<string, Func<string>> _dictMapper = new Dictionary<string, Func<string>>();


    public Worker(string inputPath, string outputPath)
    {
      _outputPath = outputPath;
      _inputPath = inputPath;
    }


    public void AddListMapper(ListMapper listMapper)
    {
      _tableMapperList.Add(listMapper);
    }

    public void SetDictMapper(Dictionary<string, Func<string>> dictMapper)
    {
      _dictMapper = dictMapper;
    }


    public void Execute()
    {
      File.Delete(_outputPath);
      File.Copy(_inputPath, _outputPath);

      var tableContents = new List<TableContent>();

      foreach (var listMapper in _tableMapperList)
      {
        var tableContent = AddTableActions(listMapper.Name, listMapper.List);
        tableContents.Add(tableContent);
      }


      var propertiesContent = AddSimpleActions(_dictMapper);
      propertiesContent.AddRange(tableContents);

      var content = new Content(propertiesContent.ToArray());

      Finalizer(content);
    }


    private void Finalizer(Content content)
    {
      using (var outputDocument = new TemplateProcessor(_outputPath)
        .SetRemoveContentControls(true))
      {
        outputDocument.FillContent(content);
        outputDocument.SaveChanges();
      }


      Document document = new Document();
      document.LoadFromFile(_outputPath);

      //Convert Word to PDF
      document.SaveToFile("toPDF.PDF", FileFormat.PDF);


      //Launch Document
      Process.Start("toPDF.PDF");


    }

    public static TableContent AddTableActions(string tablePlaceholderName, List<Dictionary<string, Func<string>>> list)
    {
      var temp = new TableContent(tablePlaceholderName);

      foreach (var dict in list)
      {
        var items = AddSimpleActions(dict).ToArray();
        temp = temp.AddRow(items);
      }

      return temp;
    }

    public static List<IContentItem> AddSimpleActions(Dictionary<string, Func<string>> dict)
    {
      var contentItems = new List<IContentItem>();


      foreach (var func in dict)
      {
        var item = new FieldContent(func.Key, func.Value());
        contentItems.Add(item);
      }

      return contentItems;
    }
  }
}