using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensSpil
{
	public static class Helper
	{

		// Borrowed from another projekt
		public static float Sin(float degrees)
		{
			return (float)Math.Sin(degrees / 180f * Math.PI);
		}

		// Borrowed from another projekt
		public static float Cos(float degrees)
		{
			return (float)Math.Cos(degrees / 180f * Math.PI);
		}

		// Borrowed from another projekt
		public static float CalculateAngleBetweenPositions(Vector2 fromPosition, Vector2 toPosition)
		{
			Vector2 vectorTowardsToVector = toPosition - fromPosition;
			float distance = vectorTowardsToVector.Length();
			if (distance > 0)
			{
				float dot = Vector2.Dot(
					new Vector2(1, 0), // Vector point right
					Vector2.Normalize(vectorTowardsToVector) // Vector pointing towards destination
				);

				float degrees = MathHelper.ToDegrees((float)Math.Acos(dot));

				if (vectorTowardsToVector.Y < 0)
				{
					degrees = 180 + (180 - degrees);
				}

				return degrees;
			}
			else
			{
				return 0;
			}
		}

	}
}
