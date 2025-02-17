shader_type canvas_item;

uniform float ShakeIntensify = 0.0;
uniform float ShakeSpeed = 20.0;

void vertex()
{
	vec2 shake = vec2(0,0);
	if (VERTEX.y < 0.0)
	{
		shake.x = sin(TIME * ShakeSpeed + VERTEX.y) * ShakeIntensify;
	}
	
	VERTEX.xy += shake;
}
