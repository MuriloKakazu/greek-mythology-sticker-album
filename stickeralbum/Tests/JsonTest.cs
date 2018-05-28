using Newtonsoft.Json;
using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.Generics;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace stickeralbum.Tests {
    class JsonTest {
        public class Vector {
            // Campos/Propriedades públicos com GET e SET públicos
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public Vector() { } // Construtor público desparametrizado

            public Vector(float x, float y, float z) {
                this.X = x; this.Y = y; this.Z = z;
            }
        }

        public class Animal {
            public Vector Geolocation;  // Campos/Propriedades públicos com GET e SET públicos
            public String Species;      // Campos/Propriedades públicos com GET e SET públicos

            public Animal() { }         // Construtor público desparametrizado

            public Animal(String species) {
                this.Species = species;
            }
        }

        public class Human : Animal {
            public String Name; // Campos/Propriedades com GET e SET públicos

            public Human() { }  // Construtor público desparametrizado

            public Human(String name) : base("Human") {
                this.Name = name;
            }
        }

        public static void Test() {
            // Serialização
            Human human0 = new Human("Roberto");
            human0.Geolocation = new Vector(-125.565f, 175.522f, 16.669f);
        
            String jsonBody = JsonConvert.SerializeObject(human0, Formatting.Indented);
            File.WriteAllText("test.json", jsonBody);

            // Desserialização
            Human human1 = JsonConvert.DeserializeObject<Human>(File.ReadAllText("test.json"));

            // Asserção (teste)
            Debug.Assert(human0.Name    == human1.Name,     "Serialization Error");
            Debug.Assert(human0.Species == human1.Species,  "Serialization Error");

            LinkedList<God> godList = new LinkedList<God>();
            LinkedList<God> maleGods = godList.Where(x => x.Gender == Gender.Male).ToLinkedList();

            Lista<Human> l0 = new Lista<Human>();
            l0.InserirNoFim(human0);
            l0.InserirNoFim(human1);
            String jsonBody1 = JsonConvert.SerializeObject(l0, Formatting.Indented);
            File.WriteAllText("test1.json", jsonBody1);

            Lista<Human> l1 = JsonConvert.DeserializeObject<Lista<Human>>(File.ReadAllText("test1.json"));
        }
    }
}
