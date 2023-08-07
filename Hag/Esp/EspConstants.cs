using EFT.CameraControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Hag.Esp
{
    class EspConstants
    {
        /* public static Vector3 WorldToScreenPoint(Vector3 worldPosition)
         {
             /*        if (Globals.ScopeCamera != null && Globals.ScopeCamera.enabled == true && Globals.LocalPlayer.HandsController.IsAiming == true)
                     {
                         return  Vector3.zero;

                     }
                     else
                     {
                         // Convert the world position to viewport space
                         Vector3 viewportPosition = Globals.MainCamera.WorldToViewportPoint(worldPosition);

                         // Calculate the screen space position from the viewport position
                         float screenX = (viewportPosition.x * Screen.width);
                         float screenY = (viewportPosition.y * Screen.height);


                         Vector3 screenPosition = new Vector3(screenX, screenY, viewportPosition.z);
                         // Optionally, you can adjust the z value of the screen position to change the depth of the object in screen space
                         //  screenPosition.z = 0;
                         screenPosition.y = -screenPosition.y;
                         screenPosition.y += Screen.height;

                         return screenPosition;
                     }

             // Convert the world position to viewport space
             Vector3 viewportPosition = Globals.MainCamera.WorldToViewportPoint(worldPosition);

             // Calculate the screen space position from the viewport position
             float screenX = (viewportPosition.x * Screen.width);
             float screenY = (viewportPosition.y * Screen.height);


             Vector3 screenPosition = new Vector3(screenX, screenY, viewportPosition.z);
             // Optionally, you can adjust the z value of the screen position to change the depth of the object in screen space
             //  screenPosition.z = 0;
             screenPosition.y = -screenPosition.y;
             screenPosition.y += Screen.height;

             return screenPosition;
         }*/
        // copy pasted from exodus666 on uc https://www.unknowncheats.me/forum/escape-from-tarkov/226519-escape-tarkov-reversal-structs-offsets-453.html https://www.unknowncheats.me/forum/3399378-post6940.html
        // what a nice guy, saved me so much sanity :) 
        static bool CheckScopeProjection(Vector2 target)
        {
            var aimingOpticSight = Globals.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.Weapon.GetComponentInChildren<OpticSight>();
            if (aimingOpticSight == null)
                return false;

            var lensMesh = aimingOpticSight.LensRenderer.GetComponent<MeshFilter>().mesh;
            if (lensMesh == null)
                return false;

            var upperRight = aimingOpticSight.LensRenderer.transform.TransformPoint(lensMesh.bounds.max);
            var upperLeft = aimingOpticSight.LensRenderer.transform.TransformPoint(new Vector3(lensMesh.bounds.min.x, 0, lensMesh.bounds.max.z));

            var upperRight_3D = Globals.MainCamera.WorldToScreenPoint(upperRight);
            var upperLeft_3D = Globals.MainCamera.WorldToScreenPoint(upperLeft);
            var scoperadius = Vector3.Distance(upperRight_3D, upperLeft_3D) / 2;
            var scopePos = Globals.MainCamera.WorldToScreenPoint(aimingOpticSight.LensRenderer.transform.position);

            var distance = Vector2.Distance(new Vector2(scopePos.x, scopePos.y), target);
            return distance <= scoperadius;
        }
        public static Vector3 WorldToScreenPoint(Vector3 worldPoint)
        {
            if (Globals.MainCamera == null)
                return Vector3.zero;
            if (Globals.LocalPlayer == null)
                return Vector3.zero;
            Vector3 screenPoint = Globals.MainCamera.WorldToScreenPoint(worldPoint);
            screenPoint.y = Screen.height - screenPoint.y;
            Vector3 OriginalScreenpoint = screenPoint;
            if (CheckScopeProjection(screenPoint))
            {
                var currentAimingMod = Globals.LocalPlayer.ProceduralWeaponAnimation.CurrentAimingMod;
                if (currentAimingMod == null)
                    return screenPoint;

                var zoom = currentAimingMod.GetCurrentOpticZoom();

                if (Globals.ScopeCamera != null && zoom > 1 && Globals.LocalPlayer.HandsController.IsAiming)
                {
                    var point = Globals.ScopeCamera.WorldToViewportPoint(worldPoint);
                    var scopePoint = Globals.ScopeCamera.ViewportToScreenPoint(point);

                    scopePoint.x += (Globals.MainCamera.pixelWidth / 2 - Globals.ScopeCamera.pixelWidth / 2);
                    scopePoint.y += (Globals.MainCamera.pixelHeight / 2 - Globals.ScopeCamera.pixelHeight / 2);
                    scopePoint.y = Screen.height - scopePoint.y;

                    if (!CheckScopeProjection(scopePoint) && Globals.Config.QualityOfLife.CalculateScopeAndScene)
                        return OriginalScreenpoint;
                    if (!CheckScopeProjection(scopePoint) && !Globals.Config.QualityOfLife.CalculateScopeAndScene)
                        return Vector3.zero;
                    return scopePoint;
                }

            }

            return screenPoint;
        }

        public static bool IsScreenPointVisible(Vector3 screenPoint)
        {
            return screenPoint.z > 0.01f && screenPoint.x > -5f && screenPoint.y > -5f && screenPoint.x < (float)UnityEngine.Screen.width && screenPoint.y < (float)UnityEngine.Screen.height;
        }
    }
}
