using System;
using System.Collections;
using System.Runtime.CompilerServices;

//Tags that identify data of XML file
public static class Tags
{
	//State tags
	public enum StateTags
	{
		NULL,
		NOT_MOVING,
		MOVING_UP,
		MOVING_DOWN
	}

	//Transition tags
	public enum TransitionTags
	{
		NULL,
		MOVE_UP_TO_MOVE_DOWN,
		MOVE_DOWN_TO_MOVE_UP,
		MOVE_UP_TO_NOT_MOVING,
		MOVE_DOWN_TO_NOT_MOVING
	}

	//EVENT TAGS
	public enum EventTags
	{
		NULL,
		BALL_FAR_AWAY,
		BALL_ABOVE_NPC,
		BALL_BELOW_NPC,
	}

	//ACTION TAGS
	public enum ActionTags
	{
		NULL,
		DONT_MOVE,
		MOVE_UP,
		MOVE_DOWN,
	}

	// <summary>
	// Get a string that has the name of a given enumeration and returns the type of enumerated value associated
	// </summary>
	// <returns>Generic enumerated value</returns>
	// <remarks> Generic lexical analyzer. Converts a lexeme into a tag with meaning </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TEnum name2Tag<TEnum>(string s)
	where TEnum : struct
	{
		TEnum resultInputType;

		Enum.TryParse(s, true, out resultInputType);
		return resultInputType;
	}
}
