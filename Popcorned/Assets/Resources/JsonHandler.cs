using System.IO;
using UnityEngine;

public class JsonHandler : MonoBehaviour
{
    private string fileName = "partitura.json";

    void Start()
    {
        // Caminho para o arquivo na pasta de dados persistentes
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Verifica se o arquivo j� existe
        if (!File.Exists(filePath))
        {
            // Carrega o arquivo JSON do Resources
            TextAsset jsonFile = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(fileName));
            if (jsonFile != null)
            {
                // Copia o conte�do do JSON para a nova localiza��o
                File.WriteAllText(filePath, jsonFile.text);
                Debug.Log($"Arquivo JSON copiado para: {filePath}");
            }
            else
            {
                Debug.LogError("O arquivo JSON n�o foi encontrado na pasta Resources.");
            }
        }
        else
        {
            Debug.Log($"Arquivo JSON j� existe em: {filePath}");
        }
    }
}
