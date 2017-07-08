using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package;
using L2Package.DataStructures;
using L2Package.Body;
using System.IO;

namespace Gosu_Packages_init
{
    class Program
    {
        static IPackageReader pf { set; get; }
        static IHeader header { set; get; }
        static IExportTable ExportTable { set; get; }
        static IImportTable ImportTable { set; get; }
        static INameTable NameTable { set; get; }

        static void Main(string[] args)
        {
            //Alloc
            pf = new PackageReader();
            pf.Read("D:\\la2\\maps\\17_21.unr");
            //pf.Read("D:\\la2\\system\\lineageeffect.u");
            header = new Header(pf.Bytes);
            ExportTable = new ExportTable(header, pf.Bytes);
            ImportTable = new ImportTable(header, pf.Bytes);
            NameTable = new NameTable(header, pf.Bytes);
            Property.Resolve = Resolver;

            //GetAllPropertyNames();
            

            Console.ReadKey();
        }

        private static void GetAllPropertyNames()
        {
            List<string> PropertyNames = new List<string>();
            List<Property> Properties = new List<Property>();
            int i = 0;
            Console.WriteLine("Gethering properties...");
            foreach (Export item in ExportTable)
            {
                int ind = (item.Class + 1) * -1;
                if (ind >= 0)
                    if (NameTable[ImportTable[ind].ObjectName] == "StaticMeshActor")
                        try
                        {
                            Properties.AddRange(Property.ReadProperties(pf.Bytes, item.SerialOffset + 0xF + 28));
                        }
                        catch (Exception ex) { }
                        

                Console.Write("\rDone {0}/{1}            ", ++i, ExportTable.Count);
            }
            Console.WriteLine();
            Console.WriteLine("Properties count: {0}", Properties.Count);
            Console.WriteLine("Gethering properties names");
            int j = 0;
            foreach (Property item in Properties)
            {
                try
                {
                    PropertyNames.Add(NameTable[item.NameTableRef]);
                }
                catch (Exception Ex)
                {
                    
                }
                Console.Write("\rDone {0}/{1}            ", ++j, Properties.Count);
            }
            PropertyNames = PropertyNames.Distinct().ToList();
            Console.WriteLine("Names count: {0}", PropertyNames.Count);
            string qwe = PropertyNames.Aggregate((a, b) => a + Environment.NewLine + b);
            File.WriteAllText("D:\\Names.txt", qwe);
        }



        private static void RE_Check_DeeperExports()
        {
            var Exp1 = ExportTable[0x16];
            int off1 = 0x9C0D;
            var Exp2 = ExportTable[0x18];
            int off2 = 0x9CB6;

            Console.WriteLine("{0:X} - {1:X} = {2}", off1, (int)Exp1.SerialOffset, off1 - Exp1.SerialOffset);
            Console.WriteLine("{0:X} - {1:X} = {2}", off2, (int)Exp2.SerialOffset, off2 - Exp2.SerialOffset);

        }
        private static void RE_ReadArray()
        {
            /*
             [NameIndex]
             [infobyte]
             [ArrayTypeNameIndex]
             */
            int Offset = 0x44C3C;
            Index Name = new Index(pf.Bytes, Offset + 2);
            Console.WriteLine("{0} => {1}", NameTable[Name], Name.Size);



        }
        private static void RE_GetExportByNameAndGroup()
        {
            Index NameRef1 = NameTable.IndexOf("MeshEmitter0");
            Export GroupRef1 = ExportTable
                .Where(E => E.NameTableRef == NameTable.IndexOf("ab_bleeding"))
                ?.First();

            int Ind = ExportTable.IndexOf(GroupRef1);

            var Exp1 = ExportTable
                .Where(E => E.NameTableRef == NameRef1)
                ?.Where(E => E.Group == Ind)
                .ToArray();
            //?.First();
            ;
            /*int RealOffset1 = 0x044C32;
            Console.WriteLine("{0:X} - {1:X} = {2:X}", 
                RealOffset1, 
                (int)Exp1.SerialOffset, 
                RealOffset1 - Exp1.SerialOffset);*/
        }
        private static void RE_GetGroups()
        {
            List<string> GroupNames = new List<string>();
            /*GroupNames.AddRange(ExportTable.Where(E => E.Group != 0).Select(E => NameTable[ExportTable[E.Group].NameTableRef]).Distinct());
            GroupNames.ForEach(N => Console.WriteLine(N));*/
            ExportTable.Select(E => E.Group)
                .Distinct()
                .Where(E => E != 0)
                .OrderByDescending(g => g)
                .Select(G => ExportTable[G - 1])
                .Select(E => E.NameTableRef)
                .Select(N => NameTable[N])
                .ToList()
                .ForEach(N => Console.WriteLine(N));
        }
        private static void RE_ExportTableMakesSence() // true
        {
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(NameTable[ExportTable[i].NameTableRef]);
            }
        }
        private static void RE_PrintNameTableMakesSence() // true
        {
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(NameTable[i]);
            }
        }
        private static void RE_Check_OffsetDeltaIsConstant() // false
        {
            string name1 = "MeshEmitter0";
            //Export Exp1 = ExportTable.Find(NameTable.IndexOf(name1));
            Export Exp1 = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name1))?.First();
            int RealOffset1 = 0x044C32;

            string name2 = "MeshEmitter25";
            //Export Exp2 = ExportTable.Find(NameTable.IndexOf(name2));
            Export Exp2 = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name2))?.First();
            int RealOffset2 = 0x0450F9;

            Console.WriteLine("{0:X} - {1:X} = {2:X}", RealOffset1, (int)Exp1.SerialOffset, RealOffset1 - Exp1.SerialOffset);
            Console.WriteLine("{0:X} - {1:X} = {2:X}", RealOffset2, (int)Exp2.SerialOffset, RealOffset2 - Exp2.SerialOffset);
        }
        static string Resolver(Index Ind)
        {
            return NameTable[Ind];
        }
        private static void RE_PrintSubProperty(Export Exp)
        {
            int PropertyOffset = 0x18 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            Prop.SetStructType(StructType.Vector);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            UVector uv = Prop.Value as UVector;
            Console.WriteLine(uv.X);
            Console.WriteLine(uv.Y);
            Console.WriteLine(uv.Z);
        }
        
    }
}
