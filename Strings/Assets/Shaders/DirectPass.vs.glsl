#version 100
precision highp float;

varying vec4 vColor;
varying vec2 vTexCoord;

attribute vec4 Pos;
attribute vec4 Color;
attribute vec2 TexCoord;

uniform mat4 Camera;

void main()
{
	vColor = Color;
	vTexCoord = TexCoord;
    gl_Position = Pos * 2.0;
}
