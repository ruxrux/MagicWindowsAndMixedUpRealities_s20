using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

public class getRequest : MonoBehaviour
{
    string location;
    string weather;

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("api.openweathermap.org/data/2.5/weather?q=Brooklyn&appid=702367da6775090551215694c7a7fea9");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);

            // read as JSON
            var result = JSON.Parse(www.downloadHandler.text);

            // parse JSON

            weather = result["weather"][0]["description"].Value;
            location = result["name"].Value;
            Debug.Log(location + " has " + weather);


        }
    }
}
