using OpenTK.Graphics.ES20;
using System;
using System.IO;
using Android.Util;

namespace Strings.Engine.Renderer
{
    class Shader
    {
        private int CompileShader(string shaderName,ShaderType type)
        {
            var src = new StreamReader(GameLoop.Context.Assets.Open("Shaders/" + shaderName + ".glsl")).ReadToEnd();
            var id = GL.CreateShader(type);
            GL.ShaderSource(id, src);
            GL.CompileShader(id);

            int state;
            GL.GetShader(id, ShaderParameter.CompileStatus, out state);
            if (state != 1)
                throw new Exception(GL.GetShaderInfoLog(id));
            return id;
        }

        ~Shader()
        {
            GameLoop.ActionQueue.Enqueue(() => GL.DeleteProgram(shaderID));
        }

        public Shader(string vsName,string fsName)
        {
            try
            {
                var e15 = GL.GetErrorCode();
                shaderID = GL.CreateProgram();
                var e14 = GL.GetErrorCode();
                
                var e9 = GL.GetErrorCode();

                var vsID = CompileShader(vsName + ".vs", ShaderType.VertexShader);
                var fsID = CompileShader(fsName + ".fs", ShaderType.FragmentShader);
                var e10 = GL.GetErrorCode();
                GL.AttachShader(shaderID, vsID);
                GL.AttachShader(shaderID, fsID);
                var e11 = GL.GetErrorCode();
                GL.LinkProgram(shaderID);
                GL.UseProgram(shaderID);
                var e12 = GL.GetErrorCode();
                GL.DeleteShader(vsID);
                GL.DeleteShader(fsID);
                var e13 = GL.GetErrorCode();

                int state;
                GL.GetProgram(shaderID, ProgramParameter.LinkStatus, out state);
                if (state != 1)
                    throw new Exception(GL.GetProgramInfoLog(shaderID));
            }
            catch(Exception ex)
            {
                Log.Error("Shader Error", ex.Message);
            }
            var e7 = GL.GetErrorCode();

            GL.ValidateProgram(shaderID);
            var e5 = GL.GetErrorCode();

            //设置着色器的各个属性
            CamLoc  = GL.GetUniformLocation(shaderID, "Camera");
            var e2 = GL.GetErrorCode();

            TexCoordLoc = GL.GetAttribLocation(shaderID, "TexCoord");
            var e1 = GL.GetErrorCode();
            ColorLoc = GL.GetAttribLocation(shaderID, "Color");
            var e3 = GL.GetErrorCode();
            PosLoc = GL.GetAttribLocation(shaderID, "Pos");
            var e4 = GL.GetErrorCode();





            GL.UseProgram(stateStack.Count > 0 ? (int)stateStack.Peek() : 0);
        }

        public void Use()
        {
            stateStack.Push(stateStack);
            GL.UseProgram(shaderID);

            var m = GameLoop.Camera;
            GL.UniformMatrix4(CamLoc, false, ref m);

            var e = GL.GetErrorCode();
        }

        public static void Unuse()
        {
            if (stateStack.Count > 1)
            {
                stateStack.Pop();
                GL.UseProgram((int)stateStack.Peek());
            }
            else
            {
                stateStack.Clear();
                GL.UseProgram(0);
            }
        }

        public int PosLoc { get; private set; }
        public int ColorLoc { get; private set; }
        public int TexCoordLoc { get; private set; }
        public int CamLoc { get; private set; }
        int shaderID;
        static System.Collections.Stack stateStack = new System.Collections.Stack();
    }
}