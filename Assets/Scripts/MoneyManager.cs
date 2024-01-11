using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public decimal currentMoney = 0;//Establecemos el contador de dinero a 0
    TextMeshProUGUI moneyText;//declaramos una variable TMPro donde irá el número en el GO "Money"
    [SerializeField]TextMeshProUGUI shopMoneyText; //Compoente que lleva la cuenta del dinero

    private void Start()
    {
        //metemos en moneyText una referencia al TextMeshPro del GO "Money"
        moneyText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        moneyText.text = currentMoney.ToString();//Convertimos la variable a STRING para que salga el numero en TMPro
        shopMoneyText.text = currentMoney.ToString();
    }
    
    public void AddMoney(float _amount)//Función para añadir dinero
    {
        currentMoney += (decimal)_amount;//suma dinero equivalente al parámetro recibido
    }
}
