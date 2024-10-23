using Identicable_Nuget.Interfaces;

namespace Identicable_Nuget
{

    //  Esta clase se me ha ocurrido ya que, a raíz de pensar si c# tenía
    //  una interfaz que nos permita identidicar de manera única un objeto
    //  sin tener que declarar una propiedad que identifique a cada objeto en una lista.

    //  Podriamos hacer que la clase alvergara generícos para permitirle al usuario que
    //  pueda trabajar que distintas formas de identificación:

    //  INT => Con números enteros
    //  STRING => Con nif o id personalizados
    public class Identificable<T> : Iidentificable<T>
    {

        public static int _iterador = 0;

        private T _id;
        public T Id
        {
            get
            {
                var defaultValue = default(T);

                if (EqualityComparer<T>.Default.Equals(_id, defaultValue))
                {
                    _id = GenerateId();
                }
                return _id;
            }

            //  El método set es pensado para en momento
            //  de insertar un id de una base de datos
            //  en el módelo

            set
            {
                _id = value;
            }
        }

        public Identificable() { }

        private T GenerateId()
        {
            T? id = default(T);
            if (id == null) id = (T)Convert.ChangeType("", typeof(T));

            Func<object> generateMethod = null;

            switch(id)
            {
                case int:

                    generateMethod = () => GenerateIdInt();

                    break;

                case string:

                    generateMethod = () => GenerateIdString();

                    break;
            }

            id = (T?)generateMethod.Invoke();

            return id;

        }
        
        //  ID TIPO INT
        private int GenerateIdInt()
        {
            _iterador += 1;
            return _iterador;
        }


        //  ID TIPO STRING

        //  Cuando queremos acceder al id de un objeto que ya tiene
        //  generado su id se vuelve a generar otro id diferente
        private string GenerateIdString()
        {
            Random random = new Random();

            string numeros = random.Next(10000000, 99999999).ToString();

            char letraInicial = ObtenerLetraAleatoria();

            char letraControl = ObtenerLetraControl(numeros);

            return $"{letraInicial}{numeros}{letraControl}";
        }
        private char ObtenerLetraAleatoria()
        {
            char[] letras = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                          'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                          'Y', 'Z' };

            Random random = new Random();
            return letras[random.Next(letras.Length)];
        }
        private char ObtenerLetraControl(string numeros)
        {
            char[] letras = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                          'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
                          'X', 'Y', 'Z' };

            // Calcular la letra a partir del número
            int numero = int.Parse(numeros);
            return letras[numero % 23];
        }
    }
}
