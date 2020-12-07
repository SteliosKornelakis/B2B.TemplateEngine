using System;
using System.Collections.Generic;
using TemplateEngine.Docx;

namespace B2B.TemplateEngine
{
  internal class Program
  {

    static AitisiKiniti aitisi = new AitisiKiniti()
    {
      FullName = "Stelios Kornelakis",
      Mobile = "6947699574",
      IsExisting = true,
      NeaSyndesi = new List<NeaSyndesi>()
      {
        new NeaSyndesi()
        {
          ServiceId = "Eimai o ServiceId",
          Armodios = "Eimai o Armodios",
          TyposYphresias = "Eimai o TyposYphresias"
        },
        new NeaSyndesi()
        {
          ServiceId = "Eimai o ServiceId 2",
          Armodios = "Eimai o Armodios 2",
          TyposYphresias = "Eimai o TyposYphresias 2"
        }
      }
    };

    private static void Main(string[] args)
    {

      // Instantiate Worker
      var worker = new Worker(@"C:\Test\initial.docx", @"C:\Test\output.docx");

      //Setup properties mappings
      var dict = new Dictionary<string, Func<string>>
      {
        {"Fullname", () => aitisi.FullName},
        {"MobileNumber", () => aitisi.Mobile},
        {"IsCurrent", () => aitisi.IsExisting ? "X" : " "},
        {"IsNew", () => aitisi.IsExisting ? " " : "X"}
      };

      //Set them
      worker.SetDictMapper(dict);


      //Create a list mapper for each table
      var listMapper = new ListMapper(@"MyTable");
      foreach (var syndesi in aitisi.NeaSyndesi)
      {
        var dictionary = new Dictionary<string, Func<string>>()
        {
          {"ServiceId", () => syndesi.ServiceId},
          {"Armodios", () => syndesi.Armodios},
          {"TyposYphresias", () => syndesi.TyposYphresias}
        };

        listMapper.List.Add(dictionary);
      }

      //Add List mapper
      worker.AddListMapper(listMapper);

      //Execute

      worker.Execute();
    }
  }
}
