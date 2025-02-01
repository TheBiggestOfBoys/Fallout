using Pip_Boy.Data_Types;
using System.Runtime.Serialization;

namespace Pip_Boy.Items
{
	/// <summary>
	/// A wearable item, which can reduce damage taken.
	/// </summary>
	[DataContract]
	public abstract class Apparel : Equipable
	{
		[DataMember]
		private readonly byte originalDamageThreshold;

		[DataMember]
		public readonly PieceType pieceType;

		/// <summary>
		/// How much damage the <see cref="Apparel"/> will negate
		/// </summary>
		public byte DamageThreshold => (byte)(originalDamageThreshold * Condition);

		/// <summary>
		/// If the <see cref="Apparel"/> object need the "Power Armor Training" <see cref="Perk"/> to be worn.
		/// </summary>
		[DataMember]
		public readonly bool RequiresPowerArmorTraining;

		#region Constructors
		/// <inheritdoc/>
		public Apparel(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects)
		{
			originalDamageThreshold = DT;
			RequiresPowerArmorTraining = powerArmor;
			pieceType = GetPieceType(this);
			Icon = IconDeterminer.Determine(this);
		}

		/// <inheritdoc/>
		public Apparel() : base() { }
		#endregion

		/// <summary>
		/// Gets
		/// </summary>
		/// <param name="apparel"></param>
		/// <returns></returns>
		public static PieceType GetPieceType(Apparel apparel)
		{
			int nothingWeight = apparel is HeadPiece ? 1 : 10;
			int lightWeight = apparel is HeadPiece ? 2 : 25;
			int mediumWeight = apparel is HeadPiece ? 7 : 30;

			return apparel.Weight switch
			{
				_ when apparel.Weight <= nothingWeight => PieceType.Nothing,
				_ when apparel.Weight <= lightWeight => PieceType.Light,
				_ when apparel.Weight <= mediumWeight => PieceType.Medium,
				_ => PieceType.Heavy,
			};
		}

		/// <summary>
		/// The approximate <see cref="DamageThreshold"/> the <see cref="Apparel"/> gives.
		/// </summary>
		public enum PieceType : byte
		{
			Nothing,
			Light,
			Medium,
			Heavy
		}
	}
}
