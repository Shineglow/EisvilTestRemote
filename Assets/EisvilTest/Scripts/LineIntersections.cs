using System.Collections.Generic;
using UnityEngine;

public static class LineIntersactions
{
    public static List<Vector3> LineCircleIntersections(Vector3 p0, Vector3 p1, Vector3 center, float radius)
    {
        Vector3 d = p1 - p0;          // Направление прямой
        Vector3 f = p0 - center;      // Вектор от центра окружности к началу отрезка

        float a = Vector3.Dot(d, d);
        float b = 2 * Vector3.Dot(f, d);
        float c = Vector3.Dot(f, f) - radius * radius;

        float discriminant = b * b - 4 * a * c;

        List<Vector3> intersections = new List<Vector3>();

        if (discriminant >= 0)
        {
            // Один или два корня
            discriminant = Mathf.Sqrt(discriminant);

            float t1 = (-b - discriminant) / (2 * a);
            float t2 = (-b + discriminant) / (2 * a);

            // Проверяем, попадают ли точки на сам отрезок (0 <= t <= 1)
            if (t1 >= 0 && t1 <= 1)
                intersections.Add(p0 + t1 * d);
            if (t2 >= 0 && t2 <= 1 && !Mathf.Approximately(t1, t2))
                intersections.Add(p0 + t2 * d);
        }

        return intersections;
    }
}