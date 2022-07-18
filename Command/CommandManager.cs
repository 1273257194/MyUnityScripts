using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CommandManager : MonoBehaviour
{
    //
    public Avatar TheAvatar;


    //命令栈实现撤销
    private Stack<Command> mCommandStack;

    private float mCallBackTime;

    //执行或撤销的标志
    public bool IsRun = true;
    private float currentTime;

    void Start()
    {
        mCommandStack = new Stack<Command>();
        mCallBackTime = 0;
        
        tempValue = interval;
    }

    public float interval;
    public float tempValue;

    void  FixedUpdate()
    {
        tempValue -= 0.02f;
        if (tempValue <= 0)
        {
            if (IsRun)
            {
                Control();
            }
            else
            {
                RunCallBack();
            } 
            tempValue = interval;
        }
    }

    //撤销
    private void RunCallBack()
    {
        mCallBackTime -= Time.deltaTime;
        //栈非空时可执行撤销操作
        if (mCommandStack.Count > 0 && mCallBackTime < mCommandStack.Peek().cTime)
        {
            mCommandStack.Pop().UnDo(TheAvatar);
        }
    }

    //执行
    private void Control()
    {
        mCallBackTime += Time.deltaTime;
        Command cmd = InputHandler();
        if (cmd != null)
        {
            //进栈
            mCommandStack.Push(cmd);
            //执行命令
            cmd.Do(TheAvatar);
        }
    }

    //命令构造：此时将请求封装为一个对象，存进命令栈
    private Command InputHandler()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return new CommandMove(new Vector3(0, 1, 0), mCallBackTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            return new CommandMove(new Vector3(0, -1, 0), mCallBackTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            return new CommandMove(new Vector3(-1, 0, 0), mCallBackTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            return new CommandMove(new Vector3(1, 0, 0), mCallBackTime);
        }

        return null;
    }
}