﻿using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

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
            File.WriteAllText("file.json", jsonBody);

            // Desserialização
            Human human1 = JsonConvert.DeserializeObject<Human>(File.ReadAllText("file.json"));

            // Asserção (teste)
            Debug.Assert(human0.Name        == human1.Name,         "Serialization Error");
            Debug.Assert(human0.Species     == human1.Species,      "Serialization Error");
            Debug.Assert(human0.Geolocation == human1.Geolocation,  "Serialization Error");
        }
    }
}
