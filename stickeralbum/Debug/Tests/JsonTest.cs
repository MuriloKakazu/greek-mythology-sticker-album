using Newtonsoft.Json;
using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.Generics;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DIAG = System.Diagnostics;

namespace stickeralbum.Debug.Tests {
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

            public Human(String name) : base(species: "Human") {
                this.Name = name;
            }
        }

        public static void TestAll() {
            // Declaration
            Human human0 = new Human(name: "Roberto");
            human0.Geolocation = new Vector(x: -125.565f, y: 175.522f, z: 16.669f);
        
            // Serialization
            String jsonBody = JsonConvert.SerializeObject(value: human0, formatting: Formatting.Indented);

            // Deserialization
            Human human1 = JsonConvert.DeserializeObject<Human>(value: jsonBody);

            // Assertion
            DebugUtils.Assert(human0.Name    == human1.Name);
            DebugUtils.Assert(human0.Species == human1.Species);
        }
    }
}
