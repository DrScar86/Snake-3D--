using UnityEngine;

public class Game : MonoBehaviour
{
    // материал стен
    public Material wallMaterial;
    // набранные очки
    public static int points;
    // количество стен в уровне
    public int countWals = 10;
    private string _pointsString;
    private int _lastPonts = -1;
    // генерируем уровень при загрузке сцены
    public void Awake()
    {
        // обнуляем очки
        points = 0;
        // генерируем уровень
        GenerateLevel();
    }

    public void Update()
    {
        // обновление текста очков только при их изменении
        if (_lastPonts == points) return;
        _lastPonts = points;
        // очки в формате четырех цифр, начинающихся с нулей
        _pointsString = "Score: " + points.ToString("0000");
    }
    // отрисовка набранных очков

    public void OnGUI()
    {
        GUI.color = Color.yellow;
        GUI.Label(new Rect(20, 20, 200, 20), _pointsString ?? "");
    }

    // функция генерации уровня
    private void GenerateLevel()
    {
        for (int i = 0; i < countWals; i++)
        {
            // создаем куб
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // называем его "Wall"
            wall.name = "Wall";
            // увеличиваем его габариты
            wall.transform.localScale = new Vector3(2, 2, 2);
            // расставляем его так, чтобы координаты были не в центре поля
            var pos = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
            while (Mathf.Abs(pos.x) < 10 || Mathf.Abs(pos.z) < 10)
            {
                pos = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
            }
            wall.transform.position = pos;
            // и назначаем материал
            //wall.renderer.material = wallMaterial;
            wall.GetComponent<Renderer>().material = wallMaterial;
        }
    }
}