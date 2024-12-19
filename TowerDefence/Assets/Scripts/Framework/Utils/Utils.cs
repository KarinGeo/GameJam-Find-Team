using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Framework
{
    public static class Utils
    {
        public const int MaxInt = 2147483647;

        public static Vector2 GetVector2(Vector3 vector)
        { 
            return new Vector2 (vector.x, vector.y);
        }

        public static Vector3 GetVector3(Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }

        public static Vector2 GetDistanceNormalized2D(Vector3 start, Vector3 target)
        {
            Vector3 distance = target - start;
            return new Vector2(distance.x, distance.y).normalized;
        }

        public static float GetDistance2D(Vector3 start, Vector3 target)
        {
            return Vector2.Distance(GetVector2(start), GetVector2(target));
        }

        //获取2D的射线检测，可以检测到UI后面的物体
        public static GameObject GetRaycast2DIgnoreUI(Vector3 mousePosition)
        {
            GameObject obj = null;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                obj = hit.collider.gameObject;
            }
            return obj;
        }

        //获取2D的射线检测，如果点击到了UI，返回true，否则返回false
        public static bool GetRaycast2D(Vector3 mousePosition, out GameObject obj)
        {
            bool isUI = false;
            obj = null;
            
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);


            if (results.Count > 0)
            {
                isUI = true;
            }
            else 
            {
                obj = GetRaycast2DIgnoreUI(mousePosition);
            }

            return isUI;
        }

        public static string FormatNumber(int number, int minDigits)
        {
            // 将数字转换为字符串，左侧填充 0 直到达到最小位数
            return number.ToString().PadLeft(minDigits, '0');
        }
    }
}

