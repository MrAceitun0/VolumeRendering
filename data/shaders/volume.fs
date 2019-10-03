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
	//Ray Set-up
	/*
		compute(u_local_camera_position);
		ray = u_local_camera_position - v_world_position;
		initial_sample = v_position;
		n_steps = 100; //Quality ??? 
		step = 0.1;
		color_acc = vec4(0,0,0,0)

		for(int i = 0; i < n_steps; i++)
		{
			if(color_acc.a < 1 && inside volume) 
			{
				color_i.rgb = color_i.rgb * color_i.a;
				color_acc = step * color_i * (1 - color_acc.a) + color_acc;
			}
		}
	*/

	//Brightness Options
	if(1.0 < u_color.x * u_brightness)
		u_color.x = 1.0;
	else
		u_color.x = u_color.x * u_brightness;

	if(1.0< u_color.y * u_brightness)
		u_color.y = 1.0;
	else
		u_color.y = u_color.y * u_brightness;

	if(1.0 < u_color.z * u_brightness)
		u_color.z = 1.0;
	else
		u_color.z = u_color.z * u_brightness;

	gl_FragColor = color_acc;
}