using JsonSerializeTests.Models;

namespace JsonSerialize.Tests
{
    [TestFixture]
    internal class SerializeTests
    {
        private Guid RowId1 { get; set; }
        private Guid RowId2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            RowId1 = Guid.NewGuid();
            RowId2 = Guid.NewGuid();
        }

        [Test]
        public void SerializeTable_WithNewtonsoft_ShouldHaveRows()
        {
            TableState instance = GetTable();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(instance);
            var item = Newtonsoft.Json.JsonConvert.DeserializeObject<TableState>(json);
            
            // Assert.That(item.ColumnStates["Produit"].GroupStates["Renault Clio"].Records["TotalActuelParProduit"], Is.EqualTo(1400));
            Assert.That(item.RowStates[RowId1].Records["Actuel"], Is.EqualTo(1200));
        }

        [Test]
        public void SerializeTable_WithSystemText_ShouldHaveRows()
        {
            TableState instance = GetTable();

            var json = System.Text.Json.JsonSerializer.Serialize(instance, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true // Pour une sortie JSON indentée (facultatif)
            });
            Console.WriteLine(json);
            var item = System.Text.Json.JsonSerializer.Deserialize<TableState>(json);

            // Assert.That(item.ColumnStates["Produit"].GroupStates["Renault Clio"].Records["TotalActuelParProduit"], Is.EqualTo(1400));
            Assert.That(item.RowStates[RowId1].Records["Actuel"], Is.EqualTo(1200));
        }

        private TableState GetTable()
        {
            TableState table = new TableState
            {
                RowStates =
                {
                    {
                        RowId1, new RowState
                        {
                            Records = new Dictionary<string, object?>
                            {
                                { "Region", "France" },
                                { "Produit", "Renault Clio" },
                                { "Actuel", 1200 }
                            }
                        }
                    },
                    {
                        RowId2, new RowState
                        {
                            Records = new Dictionary<string, object?>
                            {
                                { "Region", "Italy" },
                                { "Produit", "Renault Clio" },
                                { "Actuel", 200 }
                            }
                        }
                    }
                }
            };
            return table;
        }
    }
}
