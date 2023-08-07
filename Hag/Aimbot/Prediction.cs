using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hag.Aimbot
{


	class Prediction
	{
		// raycast hit in the class that calls form trajectory contains a list of raycasthit structs, we can change the hitmask 
		public static Vector3 PredictedPos(SDK.Players players,Vector3 bonepos)
        {
		//	if (!Esp.EspConstants.IsScreenPointVisible(bonepos))
		//		return Vector3.zero;
			
			float distance = (float)Math.Sqrt(Math.Pow(Globals.LocalPlayer.Transform.position.x - bonepos.x, 2) + Math.Pow(Globals.LocalPlayer.Transform.position.y - bonepos.y, 2) + Math.Pow(Globals.LocalPlayer.Transform.position.z - bonepos.z, 2));
			Configs.Weapon weapon = Misc.WeaponMods.GetSpeedFactorInstance(Globals.LocalPlayerWeapon.TemplateId);
			float additionalspeed = 1;
			if (weapon != null)
				additionalspeed = weapon.HitSpeedAmount;
			float travelTime = distance / (Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.InitialSpeed * additionalspeed); // Multiply initial speed by the speedfactor of instant hit
			Vector3 prediction = Globals.Offline ? players.Player.Velocity : players.Player._characterController.velocity;
			prediction.x *= travelTime; prediction.y *= travelTime; prediction.z *= travelTime; 
			prediction -= Globals.Offline ? Globals.LocalPlayer.Velocity * Time.deltaTime : Globals.LocalPlayer._characterController.velocity * Time.deltaTime; // subtract any possible movement deviation
			float factor = 1.783f; // 2.75f; // Unsure about this one, might change this up a little
			float bulletDrop = (factor * 9.81f * travelTime * travelTime);
	//		Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.BulletDiameterMilimeters = 0.1f;
		//	Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.BulletMassGram = 0.1f;
			//Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.BallisticCoeficient = 0.1f;
			prediction.y += bulletDrop / 2;

		return bonepos + prediction;

		}
	}

	/*
 float distance = sqrtf( powf( myPosition.x - TargetPlayer.location.x, 2 ) + powf( myPosition.y - TargetPlayer.location.y, 2 ) + powf( myPosition.z - TargetPlayer.location.z, 2 ) );

// Compute the aimbot prediction on the selected bone position
Vector3 predictedpos;
if ( Menu::Window.AimbotTab.Prediction.Items[0].bselected || Menu::Window.AimbotTab.Prediction.Items[1].bselected ) {
//    auto travelTime = 0.075f;
auto travelTime = distance / localPlayer.InitialAmmoSpeed;

Vector3 Prediction = Vector3( 0, 0, 0 );
if ( Menu::Window.AimbotTab.Prediction.Items[1].bselected ) {
	// [Class] -.SimpleCharacterController : MonoBehaviour, GInterface1C2F
	auto _characterController = Memory::ReadLong<uint64_t>( TargetPlayer.instance + 0x28 );
	// Speed prediction; only works in online mode and doesn't show in an offline dump...
	if ( _characterController )
		TargetPlayer.Velocity = Memory::ReadAnyType<Vector3>( _characterController + 0x48 );

	Prediction = TargetPlayer.Velocity;
	Prediction.x *= travelTime; Prediction.y *= travelTime; Prediction.z *= travelTime;

//    printf( "realVelocity: (%f, %f, %f)\n", TargetPlayer.Velocity.x, TargetPlayer.Velocity.y, TargetPlayer.Velocity.z );
}

if ( Menu::Window.AimbotTab.Prediction.Items[0].bselected ) {
	// Bullet drop prediction
	float factor = 1.783f; // 2.75f;
	float bulletDrop = factor * 9.81 * travelTime * travelTime;

	Prediction.y += bulletDrop / 2;

	// Apply the prediction to the position
	predictedpos = bone_pos + Prediction;
}
}
else
predictedpos = bone_pos;
// EFT.Player->HandsController (as FirearmController)->Item (as Weapon)->ItemTemplate (as WeaponTemplate)->_defAmmoTemplate (AmmoTemplate)->InitialSpeed (float)
// Entity + 0x420] 0x50] 0x40] 0x158] 0x174 (as float)player.GroupID == localPlayer.GroupID
// or
// EFT.Player->HandsController (as FirearmController)->Item (as Weapon)->Chambers (Slot[])->Slot[0] (Slot)->ContainedItem (as GClassXXXX)->ItemTemplate (as AmmoTemplate)->InitialSpeed (float)
// Entity + 0x420] 0x50] 0x98] 0x20] 0x38] 0x40] 0x174 (as float)

player.ammoTemplate = Memory::ReadChain( HandsController, { offsets.HandsController.ActiveWeapon, 0x40, 0x168 } ); // EFT.InventoryLogic.AmmoTemplate
player.InitialAmmoSpeed = Memory::ReadAnyType<float>( player.ammoTemplate + 0x18C ); // EFT.InventoryLogic.AmmoTemplate -> InitialSpeed : Single
static constexpr uint64_t ActiveWeapon = 0x60;
 */
	/*public sealed class ListClass<T> : IDisposable
	{
		public ListClass(int size)
		{
			this.queue_0 = new Queue<T>(size);
		
			for (int i = 0; i < size; i++)
			{
				//T item = this.func_0();
				this.queue_0.Enqueue(default(T));
			}
		}

		public T Withdraw()
		{
			T result;
			if (this.queue_0.Count > 0)
			{
				result = this.queue_0.Dequeue();
			}
			else
			{
				//result = this.func_0();
				result = default(T);
			}
			return result;
		}

		public void Return(T t)
		{
		
			this.queue_0.Enqueue(t);
		}

		public void Dispose()
		{
			
			this.queue_0.Clear();
		}

		private readonly Queue<T> queue_0;


	}

	public static class TrajContainer
	{

			
		public static TrajInfo[] Withdraw
		{
			get
			{
				return TrajContainer.List.Withdraw();
			}
		}


		public static void Return(TrajInfo[] history)
		{
			TrajContainer.List.Return(history);
		}

		private static ListClass<TrajInfo[]> List = new ListClass<TrajInfo[]>(15);
	}

	class TrajectoryCalculator
	{
		public TrajInfo Current { get; protected set; }
		public int MaxAllowedLength
		{
			get
			{
				return this.TrajectoryList.Length;
			}
		}

	public TrajectoryCalculator(Vector3 zeroPosition, Vector3 zeroVelocity, float bulletMassGram, float bulletDiameterMilimeters, float ballisticCoefficient)
	{
			try
			{
				this.Index = 0;
				TrajInfo trajinfo = new TrajInfo(0, 0f, zeroPosition, zeroVelocity);
				this.Current = trajinfo;
				this.FirstTrajectory = trajinfo;
				this.TrajectoryList =new TrajInfo[15];
				this.TrajectoryList[this.Index] = this.FirstTrajectory;
				this.BulletMassKilograms = bulletMassGram / 1000f;
				this.BulletDiameterMeters = bulletDiameterMilimeters / 1000f;
				this.BallisticCoefficient = this.BulletMassKilograms * 0.0014223f / (this.BulletDiameterMeters * this.BulletDiameterMeters * ballisticCoefficient);
				this.CrossSectionalArea = this.BulletDiameterMeters * this.BulletDiameterMeters * 3.1415927f / 4f;
				this.FrontalArea = 1.2f * this.CrossSectionalArea;
				this.Gravity = Physics.gravity;
			}
			catch (Exception ex) { System.IO.File.WriteAllText("trajcalc.txt", ex.ToString()); }
            }

        public virtual TrajInfo Next()
		{
            try { 
			this.Index++;
			TrajInfo prevTrajInfo = this.Current;
			Vector3 velocity = prevTrajInfo.velocity;
			Vector3 position = prevTrajInfo.position;
			float magnitude = velocity.magnitude;
			float dragCoefficient = Drag.CalculateDragCoefficient(magnitude) * this.BallisticCoefficient;
			Vector3 dragForce = this.FrontalArea * -dragCoefficient * magnitude * magnitude / (2f * this.BulletMassKilograms) * velocity.normalized;
			Vector3 acceleration = this.Gravity + dragForce;
			Vector3 nextPosition = position + velocity * 0.01f + 5E-05f * acceleration;
			Vector3 nextVelocity = velocity + acceleration * 0.01f;
			TrajInfo[] trajList = this.TrajectoryList;
			int trajIndex = this.Index;
			TrajInfo newTrajInfo = new TrajInfo(this.Index, prevTrajInfo.time + 0.01f, nextPosition, nextVelocity);
			this.Current = newTrajInfo;
			trajList[trajIndex] = newTrajInfo;
			
			}
			catch (Exception ex) { System.IO.File.WriteAllText("TrajNext.txt", ex.Message); }
			return this.Current;
		}
		public TrajInfo this[int index]
		{
			get
			{
				TrajInfo result = this.Current;
				if (index < this.Index)
				{
					result = this.TrajectoryList[index];
				}
				else if (this.Index < index)
				{
					while (this.Index < index)
					{
						result = this.Next();
					}
				}
				return result;
			}
		}
		public void Reset()
		{
			this.Current = this.FirstTrajectory;
			this.Index = -1;
		}
	~TrajectoryCalculator()
	{
			TrajContainer.Return(this.TrajectoryList);
		this.TrajectoryList = null;
	}
	private TrajInfo FirstTrajectory;

		protected int Index;

		private float BulletMassKilograms;

		private float BulletDiameterMeters;

		protected float BallisticCoefficient;

		protected float CrossSectionalArea;

		private float FrontalArea;

		protected float eight;

		private Vector3 Gravity;

		protected TrajInfo[] TrajectoryList;


		private TrajInfo eleven;
	}

	public readonly struct TrajInfo
	{
		public TrajInfo(int index,float time, Vector3 position, Vector3 velocity)
		{
			this.index = index;
			this.time = time;
			this.position = position;
			this.velocity = velocity;
		}
		public readonly int index;
		public readonly float time;

		public readonly Vector3 position;

		public readonly Vector3 velocity;


	}
	public struct G1Coefficent
	{
		public G1Coefficent(float _mach, float _ballist)
		{
			this.mach = _mach;
			this.ballist = _ballist;
		}

		public float mach;

		public float ballist;
	}
	class Drag
	{
		private static readonly List<G1Coefficent> G1Coefficents = new List<G1Coefficent> {
		new G1Coefficent(0f, 0.2629f),
		new G1Coefficent(0.05f, 0.2558f),
		new G1Coefficent(0.1f, 0.2487f),
		new G1Coefficent(0.15f, 0.2413f),
		new G1Coefficent(0.2f, 0.2344f),
		new G1Coefficent(0.25f, 0.2278f),
		new G1Coefficent(0.3f, 0.2214f),
		new G1Coefficent(0.35f, 0.2155f),
		new G1Coefficent(0.4f, 0.2104f),
		new G1Coefficent(0.45f, 0.2061f),
		new G1Coefficent(0.5f, 0.2032f),
		new G1Coefficent(0.55f, 0.202f),
		new G1Coefficent(0.6f, 0.2034f),
		new G1Coefficent(0.7f, 0.2165f),
		new G1Coefficent(0.725f, 0.223f),
		new G1Coefficent(0.75f, 0.2313f),
		new G1Coefficent(0.775f, 0.2417f),
		new G1Coefficent(0.8f, 0.2546f),
		new G1Coefficent(0.825f, 0.2706f),
		new G1Coefficent(0.85f, 0.2901f),
		new G1Coefficent(0.875f, 0.3136f),
		new G1Coefficent(0.9f, 0.3415f),
		new G1Coefficent(0.925f, 0.3734f),
		new G1Coefficent(0.95f, 0.4084f),
		new G1Coefficent(0.975f, 0.4448f),
		new G1Coefficent(1f, 0.4805f),
		new G1Coefficent(1.025f, 0.5136f),
		new G1Coefficent(1.05f, 0.5427f),
		new G1Coefficent(1.075f, 0.5677f),
		new G1Coefficent(1.1f, 0.5883f),
		new G1Coefficent(1.125f, 0.6053f),
		new G1Coefficent(1.15f, 0.6191f),
		new G1Coefficent(1.2f, 0.6393f),
		new G1Coefficent(1.25f, 0.6518f),
		new G1Coefficent(1.3f, 0.6589f),
		new G1Coefficent(1.35f, 0.6621f),
		new G1Coefficent(1.4f, 0.6625f),
		new G1Coefficent(1.45f, 0.6607f),
		new G1Coefficent(1.5f, 0.6573f),
		new G1Coefficent(1.55f, 0.6528f),
		new G1Coefficent(1.6f, 0.6474f),
		new G1Coefficent(1.65f, 0.6413f),
		new G1Coefficent(1.7f, 0.6347f),
		new G1Coefficent(1.75f, 0.628f),
		new G1Coefficent(1.8f, 0.621f),
		new G1Coefficent(1.85f, 0.6141f),
		new G1Coefficent(1.9f, 0.6072f),
		new G1Coefficent(1.95f, 0.6003f),
		new G1Coefficent(2f, 0.5934f),
		new G1Coefficent(2.05f, 0.5867f),
		new G1Coefficent(2.1f, 0.5804f),
		new G1Coefficent(2.15f, 0.5743f),
		new G1Coefficent(2.2f, 0.5685f),
		new G1Coefficent(2.25f, 0.563f),
		new G1Coefficent(2.3f, 0.5577f),
		new G1Coefficent(2.35f, 0.5527f),
		new G1Coefficent(2.4f, 0.5481f),
		new G1Coefficent(2.45f, 0.5438f),
		new G1Coefficent(2.5f, 0.5397f),
		new G1Coefficent(2.6f, 0.5325f),
		new G1Coefficent(2.7f, 0.5264f),
		new G1Coefficent(2.8f, 0.5211f),
		new G1Coefficent(2.9f, 0.5168f),
		new G1Coefficent(3f, 0.5133f),
		new G1Coefficent(3.1f, 0.5105f),
		new G1Coefficent(3.2f, 0.5084f),
		new G1Coefficent(3.3f, 0.5067f),
		new G1Coefficent(3.4f, 0.5054f),
		new G1Coefficent(3.5f, 0.504f),
		new G1Coefficent(3.6f, 0.503f),
		new G1Coefficent(3.7f, 0.5022f),
		new G1Coefficent(3.8f, 0.5016f),
		new G1Coefficent(3.9f, 0.501f),
		new G1Coefficent(4f, 0.5006f),
		new G1Coefficent(4.2f, 0.4998f),
		new G1Coefficent(4.4f, 0.4995f),
		new G1Coefficent(4.6f, 0.4992f),
		new G1Coefficent(4.8f, 0.499f),
		new G1Coefficent(5f, 0.4988f)

		};
		public static float CalculateDragCoefficient(float velocity)
		{
			int DragIndex = (int)Mathf.Floor(velocity / 343f / 0.05f);
			if (DragIndex <= 0)
				return 0f;

			if (DragIndex > G1Coefficents.Count() - 1)
			{
				return G1Coefficents[G1Coefficents.Count() - 1].ballist;
			}
			float PreviousDrag = G1Coefficents[DragIndex - 1].mach * 343f;
			float CurrentDrag = G1Coefficents[DragIndex].mach * 343f;
			float Ballist = G1Coefficents[DragIndex - 1].ballist;
			return (G1Coefficents[DragIndex].ballist - Ballist) / (CurrentDrag - PreviousDrag) * (velocity - PreviousDrag) + Ballist;
		}
	}*/
}
