using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// скрипту игрока необходим на объекте компонент CharacterController
// с помощью этого компонента будет выполн€тьс€ движение
[RequireComponent(typeof(CharacterController))]
public class Player2 : MonoBehaviour
{
    // скорость перемещени€ - 6 единиц в секунду по умолчанию
    // в редакторе можно помен€ть
    public float speed = 10;
    // аналогично скорость вращени€ 60 градусов в секунду по умолчанию
    public float rotationSpeed = 160;
    // локальна€ переменна€ дл€ хранени€ ссылки на компонент CharacterController
    private CharacterController _controller;
    public UnityEvent OnEat;
    public void Start()
    {
        // получаем компонент CharacterController и 
        // записываем его в локальную переменную
        _controller = GetComponent<CharacterController>();
        // создаем хвост
        // current - текуща€ цель элемента хвоста, начинаем с головы
        Transform current = transform;
        for (int i = 0; i < 3; i++)
        {
            // создаем примитив куб и добавл€ем ему компонент Tail
            Tail tail = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Tail>();
            // помещаем "хвост" за "хоз€ином"
            tail.transform.position = current.transform.position - current.transform.forward * 2;
            // ориентаци€ хвоста как ориентаци€ хоз€ина
            tail.transform.rotation = transform.rotation;
            // элемент хвоста должен следовать за хоз€ином, поэтому передаем ему ссылку на его
            tail.target = current.transform;
            // дистанци€ между элементами хвоста - 2 единицы
            tail.targetDistance = 2;
            // удал€ем с хвоста коллайдер, так как он не нужен
            //Destroy(tail.collider);
            Destroy(tail.GetComponent<Collider>());
            // следующим хоз€ином будет новосозданный элемент хвоста
            current = tail.transform;
        }
    }
    private bool _testing = false;
    public void Update()
    {
        /* 
        * √ибкий способ - использовать оси
        * Unity имеет набор предустановленных осей, которые можно использовать
        * следующий код будет работать как на клавиатуре (стрелки и WSAD), так и на геймпаде
        */
        // получаем значение вертикальной оси ввода
        /* float vertical = Input.GetAxis("Vertical"); */
        // получаем значение горизонтальной оси ввода
        float horizontal = Input.GetAxis("Horizontal");

        // вращаем трансформ вокруг оси Y 
        transform.Rotate(0, rotationSpeed * Time.deltaTime * horizontal, 0);
        // движение выполн€ем с помощью контроллера в сторону, куда смотрит трансформ игрока
        // двигаем змею посто€нно
        _controller.Move(transform.forward * speed * Time.deltaTime /* * vertical*/);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(5);
        }
    }
    GameObject food;
    // ¬ данную функцию будут передаватьс€ все объекты, с которыми
    // CharacterController вступает в столкновени€
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Food")
        {
            // прибавл€ем очки еды к общему числу очков
            Game.points += 10;
            //¬резались в еду, "съедаем" ее и создаем новую в пределах пол€. 
            //Ќа самом деле перемещаем еду в Random положение
            food = GameObject.Find("Food");
            //Destroy(food);
            var pos = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
            food.transform.position = pos;
            if (OnEat != null)
            {
                OnEat.Invoke();
            }
        }
        else
        {
            //врезались не в еду
            Application.LoadLevel("GameOver");
        }
    }
}