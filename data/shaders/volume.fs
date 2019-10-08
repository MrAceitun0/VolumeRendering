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
    vec3 init_pos = v_position;

    //Ray
    vec3 ray = v_position - u_local_camera_position;
    vec3 dir = normalize(ray);
    vec3 step_vector = dir * u_quality;

    //color
    vec4 color_acc = vec4(0.0, 0.0, 0.0, 0.0);
    vec4 color_i = vec4(0.0, 0.0, 0.0, 0.0); 

    while(1)
    {
        init_pos += step_vector;
        vec3 init_pos_norm = (init_pos + 1) / 2.0;

        if (init_pos.x > 1 || init_pos.y > 1 || init_pos.z > 1 || init_pos.x < -1 || init_pos.y < -1 || init_pos.z < -1 )
        { 
            break;
        }

        color_i = texture3D(u_texture, init_pos_norm);
        color_i = vec4(u_color.xyz, color_i.x);
        color_i.rgb = color_i.rgb * color_i.a;
        color_acc += length(step_vector) * color_i * (1.0 - color_acc.a + u_brightness / 100);

        if(color_acc.a > 1.0)
            break;
    }

    //For Volume Rendering
    gl_FragColor = color_acc;
}