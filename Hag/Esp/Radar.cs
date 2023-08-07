using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using System.Collections;
using Hag.Renderer;
using EFT.InventoryLogic;
using EFT.Interactive;
using System.Threading;
using EFT.UI;
using System.Reflection;
using System.IO;
using Hag.Helpers;
using EFT.Weather;

namespace Hag.Esp
{
    class Radar : MonoBehaviour
    {
	

		[ObfuscationAttribute(Exclude = true)]
		void Start()
		{
			StartObfuscated();
		}
		void StartObfuscated()
		{
		//	StartCoroutine(UpdateRadar());
		}

		IEnumerator UpdateRadar()
		{ 
		for (; ; )
            {
				if (Globals.GameWorld == null || Globals.LocalPlayer == null)
					goto END;
				CameraSet = SetupCamera(!CameraSet);

				if (RadarObject != null)
				{
					RadarObject.transform.eulerAngles = new Vector3(90, Globals.MainCamera.transform.eulerAngles.y, Globals.MainCamera.transform.eulerAngles.z);
					RadarObject.transform.localPosition = new Vector3(Globals.MainCamera.transform.localPosition.x, 100 * Mathf.Tan(45), Globals.MainCamera.transform.localPosition.z);
				}
				END:
                yield return new WaitForEndOfFrame();
			}
		}
		 bool CameraSet = false;
		 GameObject RadarObject;
		 Camera RadarCamera;
		private bool SetupCamera(bool Set)
		{
			if (Set)
			{
				var weatherController = WeatherController.Instance;
				if (weatherController != null)
				{
					var weatherDebug = weatherController.WeatherDebug;
					weatherDebug.Enabled = true;
					weatherDebug.CloudDensity = -0.7f;
					weatherDebug.Fog = 0.004f;
					weatherDebug.LightningThunderProbability = 0f;
					weatherDebug.Rain = 0f;
				}

				var sky = TOD_Sky.Instance;
				if (sky != null)
				{

					sky.Components.Time.GameDateTime = null;
					sky.Cycle.Hour = 12f;
				}
					//Create Radar Camera
				if (RadarCamera == null)
				{
					RadarObject = new GameObject("RadarCamera", new Type[] { typeof(Camera), typeof(PrismEffects) });
					RadarObject.GetComponent<PrismEffects>().CopyComponentValues(Globals.MainCamera.GetComponent<PrismEffects>());
					RadarCamera = RadarObject.GetComponent<Camera>();
					RadarCamera.name = "RadarCam";
					RadarCamera.pixelRect = new Rect(100, 100, 100, 100);
					RadarCamera.allowHDR = false;
					RadarCamera.depth = -1;
				}
				return true;

			}
			return !Set;
		}
		private Vector2 WorldToRadarPoint(Vector3 enemyPosition)
		{
			float enemyy = Globals.MainCamera.transform.position.x - enemyPosition.x;
			float enemyx = Globals.MainCamera.transform.position.z - enemyPosition.z;
			float enemyatan = Mathf.Atan2(enemyy, enemyx) * Mathf.Rad2Deg - 270 - Globals.MainCamera.transform.eulerAngles.y;

			var enemydistance = Mathf.Round(Vector3.Distance(Globals.MainCamera.transform.position, enemyPosition));

			float enemyradarx = enemydistance * Mathf.Cos(enemyatan * Mathf.Deg2Rad);
			float enemyradary = enemydistance * Mathf.Sin(enemyatan * Mathf.Deg2Rad);

			enemyradarx = enemyradarx * (Globals.Config.Radar.Size / Globals.Config.Radar.Size) / 2f;
			enemyradary = enemyradary * (Globals.Config.Radar.Size / Globals.Config.Radar.Size) / 2f;

			return new Vector2(Globals.Config.Radar.Radarx+ enemyradarx, Globals.Config.Radar.Radary  + enemyradary);
		}
		/*private Vector2 WorldToMapPoint()
		{ 
	
		}*/


		
		public void DrawRadarEnemy(Direct2DRenderer renderer,Direct2DFont font)
		{
			if (!Globals.Config.Radar.Enable || Globals.MainCamera == null || Globals.GameWorld == null)
				return;
			Color32 primary = Helpers.ColourHelper.GetColour("Radar Primary Colour");
			Color32 secondary = Helpers.ColourHelper.GetColour("Radar Secondary Colour");
			if (Globals.Config.Radar.Background)
			{
				

				renderer.FillCircle(Globals.Config.Radar.Radarx, Globals.Config.Radar.Radary, Globals.Config.Radar.Size, new Direct2DColor(secondary.r, secondary.g, secondary.b, secondary.a));

			}
			
			renderer.DrawLine(Globals.Config.Radar.Radarx, Globals.Config.Radar.Radary + Globals.Config.Radar.Size, Globals.Config.Radar.Radarx, Globals.Config.Radar.Radary - Globals.Config.Radar.Size, 1f, new Direct2DColor(primary.r, primary.g, primary.b, primary.a));
			renderer.DrawLine(Globals.Config.Radar.Radarx + Globals.Config.Radar.Size , Globals.Config.Radar.Radary, Globals.Config.Radar.Radarx - Globals.Config.Radar.Size, Globals.Config.Radar.Radary , 1f, new Direct2DColor(primary.r, primary.g, primary.b, primary.a));
			renderer.DrawCircle(Globals.Config.Radar.Radarx, Globals.Config.Radar.Radary, Globals.Config.Radar.Size, 1, new Direct2DColor(primary.r, primary.g, primary.b, primary.a));
			foreach (SDK.Players players in Globals.PlayerDict.Values)
			{
			
				if (players.Player == null || !players.Player.HealthController.IsAlive)
					continue;
			//	Globals.LocalPlayer.Look(0, 0, false);
			//	Globals.LocalPlayer.PlayerBones.AnimatedTransform.rotation = Quaternion.Euler(0, 0, 0);
				//players.Player.PlayerBones.AnimatedTransform.rotation = Quaternion.Euler(0, 0, 0);
				//	players.Player.ProceduralWeaponAnimation.SetHeadRotation(Vector3.zero);
				//Globals.LocalPlayer.MovementContext.SetDirectlyLookRotations(Vector2.zero, Vector2.zero);
				//players.Player.MovementContext.SetDirectlyLookRotations(Vector2.zero, Vector2.zero);
				//	players.Player.MovementContext.SetPrivateField("", Vector3.zero);
				//	players.Player.MovementContext.SetPrivateField("", Vector3.zero);

				

			
				Color32 colour = Color.red;
				bool visible = false;
				bool name = false;
				bool distance = false;
				bool weapon = false;
				int fontsize = 9;
				int mindist = 50;
				int maxdist = 1000;
				foreach (bool bone in players.BoneVisible)
				{
					if (bone)
						visible = true;
				}
				if (players.PlayerType == SDK.PlayerType.Player)
				{
					if (!Globals.Config.PlayerRadar.Enable)
						continue;

						if (visible)
						colour = Helpers.ColourHelper.GetColour("Radar Player Visible Colour");
					else
						colour = Helpers.ColourHelper.GetColour("Radar Player Invisible Colour");
				
						name = Globals.Config.PlayerRadar.Name;
					distance = Globals.Config.PlayerRadar.Distance;
					weapon = Globals.Config.PlayerRadar.Weapon;
					fontsize = Globals.Config.PlayerRadar.FontSize;
					mindist = Globals.Config.PlayerRadar.MinimumTextDistance;
					maxdist = Globals.Config.PlayerRadar.MaximumTextDistance;
				}
				if (players.PlayerType == SDK.PlayerType.Scav)
				{
					if (!Globals.Config.ScavRadar.Enable)
						continue;
					if (visible)
						colour = Helpers.ColourHelper.GetColour("Radar Scav Visible Colour");
					else
						colour = Helpers.ColourHelper.GetColour("Radar Scav Invisible Colour");
					name = Globals.Config.ScavRadar.Name;
					distance = Globals.Config.ScavRadar.Distance;
					weapon = Globals.Config.ScavRadar.Weapon;
					fontsize = Globals.Config.ScavRadar.FontSize;
					mindist = Globals.Config.ScavRadar.MinimumTextDistance;
					maxdist = Globals.Config.ScavRadar.MaximumTextDistance;
				}
				if (players.PlayerType == SDK.PlayerType.Boss)
				{
					if (!Globals.Config.BossRadar.Enable)
						continue;
					if (visible)
						colour = Helpers.ColourHelper.GetColour("Radar Boss Visible Colour");
					else
						colour = Helpers.ColourHelper.GetColour("Radar Boss Invisible Colour");
					name = Globals.Config.BossRadar.Name;
					distance = Globals.Config.BossRadar.Distance;
					weapon = Globals.Config.BossRadar.Weapon;
					fontsize = Globals.Config.BossRadar.FontSize;
					mindist = Globals.Config.BossRadar.MinimumTextDistance;
					maxdist = Globals.Config.BossRadar.MaximumTextDistance;
				}
				if (players.PlayerType == SDK.PlayerType.ScavPlayer)
				{
					if (!Globals.Config.ScavPlayerRadar.Enable)
						continue;
					if (visible)
						colour = Helpers.ColourHelper.GetColour("Radar Scav Player Visible Colour");
					else
						colour = Helpers.ColourHelper.GetColour("Radar Scav Player Invisible Colour");
					name = Globals.Config.ScavPlayerRadar.Name;
					distance = Globals.Config.ScavPlayerRadar.Distance;
					weapon = Globals.Config.ScavPlayerRadar.Weapon;
					fontsize = Globals.Config.ScavPlayerRadar.FontSize;
					mindist = Globals.Config.ScavPlayerRadar.MinimumTextDistance;
					maxdist = Globals.Config.ScavPlayerRadar.MaximumTextDistance;
				}
				Vector2 radarpos = WorldToRadarPoint(players.Player.Transform.position);
				if (Vector2.Distance(new Vector2(radarpos.x, radarpos.y), new Vector2(Globals.Config.Radar.Radarx, Globals.Config.Radar.Radary)) > (float)Globals.Config.Radar.Size - 10)
					continue;
				string playername = name ? players.Player.Profile.Nickname.Localized() : "";
				if (players.PlayerType == SDK.PlayerType.ScavPlayer && name)
					playername = "Scav Player";
				if (players.PlayerType == SDK.PlayerType.Scav && name)
					playername = "Scav";
				if (players.PlayerType == SDK.PlayerType.Boss && name)
					playername = PlayerEsp.GetBossName(players);
				string playerdistance = distance ? string.Concat("[", players.Distance, "m]") : "";
				string playerweapon = "";
				if (players.Weapon != null)
					playerweapon = weapon ? players.Weapon.ShortName.Localized() : "";
				renderer.FillCircle(radarpos.x, radarpos.y, 3f,new Direct2DColor(colour.r, colour.g, colour.b, colour.a));
				if(players.Distance > mindist && players.Distance < maxdist)
					renderer.DrawTextCentered(string.Concat( playername, playerdistance,playerweapon), radarpos.x, radarpos.y - 12, fontsize, font, new Direct2DColor(colour.r, colour.g, colour.b, colour.a));
			
			}
		}
	}

	
}
