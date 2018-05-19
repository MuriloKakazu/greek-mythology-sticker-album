using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Tests {
    class Lista<T> : IEnumerator, IEnumerable {
        Nodo<T> nodoAtualParaLista = null;
        Nodo<T> primeiro = null;
        int qtde = 0;

        public Lista() {

        }

        public void InserirNaPosicao(Nodo<T> anterior,
                                     T valor) {
            Nodo<T> novo = new Nodo<T>();
            novo.Dado = valor;

            if (anterior == null) {
                if (primeiro == null)
                    primeiro = novo;
                else {
                    novo.Proximo = primeiro;
                    primeiro = novo;
                }
            } else {
                novo.Proximo = anterior.Proximo;
                anterior.Proximo = novo;
            }
            qtde++;
        }

        public void InserirNoInicio(T valor) {
            InserirNaPosicao(null, valor);
        }


        public void InserirNoFim(T valor) {
            if (qtde == 0) {
                InserirNoInicio(valor);
            } else {
                Nodo<T> aux = primeiro;
                while (aux.Proximo != null) {
                    aux = aux.Proximo;
                }
                InserirNaPosicao(aux, valor);
            }
        }


        /// <summary>
        /// Insere em uma posição, iniciando do 1
        /// </summary>
        /// <param name="valor">valor</param>
        /// <param name="posicao">posicao iniciando do 1</param>
        public void InserirNaPosicao(T valor, int posicao) {
            if (posicao > qtde || posicao <= 0)
                throw new Exception("Não é possível inserir.");

            if (posicao == 1)
                InserirNoInicio(valor);
            else {
                Nodo<T> aux = primeiro;
                for (int i = 1; i < posicao - 1; i++)
                    aux = aux.Proximo;

                InserirNaPosicao(aux, valor);
            }
        }


        public object RemoverDaPosicao(int posicao) {
            if (posicao > qtde || posicao <= 0)
                throw new Exception("Não é possível remover.");

            object valor = null;
            if (posicao == 1) {
                valor = primeiro.Dado;
                primeiro = primeiro.Proximo;
            } else {
                Nodo<T> aux = primeiro;
                for (int i = 1; i < posicao - 1; i++)
                    aux = aux.Proximo;

                valor = aux.Proximo.Dado;
                aux.Proximo = aux.Proximo.Proximo;
            }
            qtde--;
            return valor;
        }



        public bool Pesquisa(T dado, IComparer comparador) {
            Nodo<T> aux = primeiro;
            while (aux != null) {
                if (comparador.Compare(aux.Dado, dado) == 0)
                    return true;
                aux = aux.Proximo;
            }
            return false;
        }



        public string Listar() {
            string r = string.Empty;

            Nodo<T> aux = primeiro;
            while (aux != null) {
                r = r + Environment.NewLine + aux.Dado.ToString();

                aux = aux.Proximo;
            }

            return r.Trim();
        }



        public void Listar(Nodo<T> e) {
            if (e != null)
                Console.WriteLine(e.Dado);

            if (e.Proximo != null)
                Listar(e.Proximo);
        }


        public Nodo<T> RetornaPrimeiro() {
            return primeiro;
        }



        public object Current {
            get {
                if (nodoAtualParaLista == null)
                    return null;
                else
                    return nodoAtualParaLista.Dado;
            }
        }

        public bool MoveNext() {
            if (nodoAtualParaLista == null)
                nodoAtualParaLista = primeiro;
            else
                nodoAtualParaLista = nodoAtualParaLista.Proximo;

            bool retorno = nodoAtualParaLista != null;
            return retorno;
        }

        public void Reset() {
            nodoAtualParaLista = null;
        }


        #region IEnumerable Members

        public IEnumerator GetEnumerator() {
            Reset();
            return (IEnumerator) this;
        }

        #endregion
    }
}
