using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Tests
{
    public class Nodo<T>
    {
        public Nodo<T> Proximo;
        public Nodo<T> Anterior;
        public T Dado;
    }
}
