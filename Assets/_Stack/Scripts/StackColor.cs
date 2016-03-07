using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// A class to create new pastel color easily
/// </summary>
[Serializable]
public struct StackColor 
{
    public float h,s,l,a;
    public StackColor(float h, float s, float l, float a = 1){this.h = h; this.s = s; this.l = l; this.a = a;}
    public StackColor(Color c){StackColor temp = Utils.FromColor(c); h = temp.h; s = temp.s; l = temp.l; a = temp.a;}
	public static implicit operator StackColor(Color src){return Utils.FromColor(src);}
	public static implicit operator Color(StackColor src){return Utils.ToColor(src);}
}
