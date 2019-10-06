varying vec3 v_position;
varying vec3 v_world_position;

//Texture space is [0, 1]
uniform sampler3D u_texture;

//Camera position
uniform vec3 u_camera_position;
uniform vec3 u_local_camera_position;	//You can use this now for the algorithm, in the assigment you will be responsible to compute it

//Optional to use
uniform float u_quality;
uniform float u_brightness;
uniform vec4 u_color;

void main()
{
	//Ray
	vec3 ray = u_local_camera_position - v_position;
	vec3 current_sample = (v_position + 1) / 2;
	vec3 dir = normalize(ray);
	float step = u_quality;
	vec3 step_vector = dir * step;

	vec4 color_acc = vec4(0,0,0,0); 

	for(int i = 0; i < 4000; i++)
	{
		float v = texture3D(u_texture, current_sample);
		vec4 color_i = vec4(v.x, v.x, v.x, v.x);

		color_i.rgb = color_i.rgb * color_i.a;
		color_acc = step * color_i * (1.0 - color_acc.a) + color_acc;

		if(color_acc.a >= 1.0 || texture3D(u_texture, current_sample) == 0)
			break;	

		current_sample += step_vector;
	}
	
	//Brightness Options
	/*
	if(1.0 < color_acc.x * u_brightness)
		color_acc.x = 1.0;
	else
		color_acc.x = color_acc.x * u_brightness;

	if(1.0< color_acc.y * u_brightness)
		color_acc.y = 1.0;
	else
		color_acc.y = color_acc.y * u_brightness;

	if(1.0 < color_acc.z * u_brightness)
		color_acc.z = 1.0;
	else
		color_acc.z = color_acc.z * u_brightness;
	*/
	//gl_FragColor = u_color;
	
	//For Volume Rendering
	gl_FragColor = color_acc;
}