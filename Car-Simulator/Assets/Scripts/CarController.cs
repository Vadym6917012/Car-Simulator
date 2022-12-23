
using UnityEngine;


public class CarController : MonoBehaviour
{
    //Охогошуємо постійні константи для імен осей
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;//Для зберігання наших фактичних вхідних значень
    private float verticalInput;//Для зберігання наших фактичних вхідних значень
    private float currentSteerAngle;//Для зберыгання нашого поточного кута потороту
    private float currentbreakForce;//Гальмівна сила
    private bool isBreaking;//Привчастий вхід при натичкані Space

    //Постійна моторна сила
    [SerializeField] private float motorForce;
    //Тимчасова гальмівна сила
    [SerializeField] private float breakForce;
    //Збереження нашого поточного кута повороту
    [SerializeField] private float maxSteerAngle;

    //Силки на всі компоненти колесного колайдеру
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    //Переобразування візуалізації коліс
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    //Ми використовуємо фізичний двіжок, тому використовуємо метод FixedUpdate
    private void FixedUpdate()
    {
        GetInput();//Дані користувача
        HandleMotor();//Посиленя на колеса
        HandleSteering();//Обробка обертання коліс
        UpdateWheels();//Обновлення візуального ефекту коліс
    }

    //Ми призначаем значення з вхідних методів, до наших горизонтальних і вертикальних входів
    //яикм призначаєм назву осі як параметр
    private void GetInput()
    {
        //Приймаємо ім'я осі в качестві параметра
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        //Додаємо гальмівний вхід під час натискання Space
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    //Метод ручного двигуна, який керує колесними колайдерами
    private void HandleMotor()
    {
        //Даємо силу тільки на передні колеса (так як в нас машина з переднім приводом)
        //Встановлюємо значення сили двигуна в колесні колайдери
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        //Тимчасове значення гальмівної сили
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    //Призначаємо гальмівну силу всіх колайдерів
    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    //Рульове управління
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    //Запроваджаємо метод обновляющий візуальні ефекти колеса
    private void UpdateWheels()
    {
        //Параметрами ми беремо колесний колайдер і переотворення, які переобразовують з інспектора
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    //Отримуєм потрібне положення та обертання
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        //Цей метод бере вектор 3 і кватерніон як вхідні паратри та надсилає на основні змінні, внесених колейдером коліс
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}

