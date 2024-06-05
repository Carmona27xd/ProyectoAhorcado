using AhorcadoServicios.Model.DTO;
using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AhorcadoServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WordService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WordService.svc o WordService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WordService : IWordService
    {
        public void DoWork()
        {
        }

        public List<Category> GetCategories()
        {
            return CategoryDTO.getCategories();
        }

        public string getWordEnglish(int wordID)
        {
            return WordDTO.getWordEnglish(wordID);
        }

        public string getWordSpanish(int wordID)
        {
            return WordDTO.getWordSpanish(wordID);
        }

        public List<Word> GetWordsPerCategory(int category)
        {
            return WordDTO.getWordsPerCategory(category);
        }
    }
}
