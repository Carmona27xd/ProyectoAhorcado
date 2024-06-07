using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AhorcadoServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWordService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWordService
    {
        [OperationContract]
        List<Category> GetCategories();
        [OperationContract]
        List<Word> GetWordsPerCategory(int category);
        [OperationContract]
        string getWordSpanish(int wordID);
        [OperationContract]
        string getWordEnglish(int wordID); 
        [OperationContract]
        string getClueSpanish(int wordID);
        [OperationContract]
        string getClueEnglish(int wordID);
    }
}
