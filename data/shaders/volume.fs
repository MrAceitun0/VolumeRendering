varying vec3 v_position;
varying vec3 v_world_position;

//Texture space is [0, 1]
uniform sampler3D u_texture;

//Camera position
uniform vec3 u_camera_position;
uniform vec3 u_local_camera_position;   //You can use this now for the algorithm, in the assigment you will be responsible to compute it

//Optional to use
uniform float u_quality;
uniform float u_brightness;
uniform vec4 u_color;

void main()
{
    vec3 current_sample = v_position;

    //Ray
    vec3 step_vector = normalize(v_position - u_local_camera_position) * u_quality;

    //color
    vec4 color_acc = vec4(0.0, 0.0, 0.0, 0.0);

    while(1)
    {
        current_sample += step_vector;
        vec3 current_sample_norm = (current_sample + 1) / 2.0;

        vec4 color_i = texture3D(u_texture, current_sample_norm);
        color_i = vec4(u_color.xyz, color_i.x);
        color_i.rgb = color_i.rgb * color_i.a;
        color_acc = length(step_vector) * color_i * (1.0 - color_acc.a) + color_acc;

        if(color_acc.a > 1.0)
            break;

        if (current_sample.x > 1 || current_sample.y > 1 || current_sample.z > 1 || current_sample.x < -1 || current_sample.y < -1 || current_sample.z < -1 ) 
            break;
    }

	//Brightness Options
	if(1.0 < u_color.x * u_brightness)
		u_color.x = 1.0;
	if(1.0 < color_acc.x * u_brightness)
		color_acc.x = 1.0;
	else
		u_color.x = u_color.x * u_brightness;
		color_acc.x = color_acc.x * u_brightness;

	if(1.0< u_color.y * u_brightness)
		u_color.y = 1.0;
	if(1.0< color_acc.y * u_brightness)
		color_acc.y = 1.0;
	else
		u_color.y = u_color.y * u_brightness;
		color_acc.y = color_acc.y * u_brightness;

	if(1.0 < u_color.z * u_brightness)
		u_color.z = 1.0;
	if(1.0 < color_acc.z * u_brightness)
		color_acc.z = 1.0;
	else
		u_color.z = u_color.z * u_brightness;
		color_acc.z = color_acc.z * u_brightness;

    //For Volume Rendering
    gl_FragColor = color_acc;
}