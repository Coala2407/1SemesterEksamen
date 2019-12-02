using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EksamensSpil
{
	class SwordSwing : GameObject
	{
		private float movementSpeed;
		private Vector2 movement;
		private float range;
		private float swingTime;
		private bool enemyHit = false;


		public SwordSwing(Sword sword, float movementSpeed)
		{

			this.movementSpeed = movementSpeed;
			this.position = sword.Position + sword.ForwardVector * 50;
			this.range = sword.Range;
			this.swingTime = sword.SwingTime;
			movement = Crosshair.currentPosition - position;
			rotation = Helper.CalculateAngleBetweenPositions(position, Crosshair.currentPosition);
			ChangeSprite(Assets.SwingEffectSprite);

		}

		public override void LoadContent(ContentManager content)
		{
			throw new NotImplementedException();
		}

		public override void OnCollision(GameObject otherObject)
		{
			Enemy enemy = otherObject as Enemy;

			if(enemy != null && enemyHit == false)
			{
				enemy.UpdateHealth(2);
				enemyHit = true;
			}
		}

		public override void Update(GameTime gameTime)
		{
			SwingEffect(gameTime);
		}

		// TODO: Need to make the sword.range more universal if more swords are added
		public void SwingEffect(GameTime gameTime)
		{
			swingTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

			//Normalizes movement of the swing, ensuring it moves in one direction
			if (movement != Vector2.Zero)
			{
				movement.Normalize();
			}

			//Gives the swing movement
			if(position.X < GameWorld.Player.Position.X + range && 
			   position.Y < GameWorld.Player.Position.Y + range &&
			   position.X > GameWorld.Player.Position.X - range && 
			   position.Y > GameWorld.Player.Position.Y - range)
			{
				position += movement * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}

			if(swingTime <= 0)
			{
				GameWorld.RemoveGameObject(this);
			}
		}
	}
}
