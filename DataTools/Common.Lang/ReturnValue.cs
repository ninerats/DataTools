using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;

namespace Craftsmaneer.Lang
{

    public class ReturnValue
    {
        public bool Success { get; protected set; }
        public Exception Error { get; protected set; }
        public ReturnValue Inner { get; protected set; }
        public string Context { get; protected set; }


        private static ReturnValue _successful = new ReturnValue(true);
       

        /// <summary>
        /// default to successful result.
        /// </summary>
        protected ReturnValue()
            : this(true)
        {

        }

        public ReturnValue(bool success, string context = "", Exception exception = null)
        {
            Success = success;
            Context = context;
            Error = exception;

        }

        public static ReturnValue Wrap(Action action, string context = "")
        {
            try
            {
                action();
                var r = SuccessResult();
                return r;
            }
            catch (AbortException abort)
            {

                return FailResult(string.Format("FAIL {0} -> {1}", context, abort.Caller));
            }
            catch (Exception ex)
            {
                // log it.
                var r = FailResult(context == "" ? "wrapped action failed" : context, ex);
                return r;
            }
        }

      

        public static ReturnValue SuccessResult()
        {
            return new ReturnValue();
        }

        public static ReturnValue FailResult(string context = "", Exception exception = null)
        {
            return new ReturnValue(false, context, exception);
        }

        /// <summary>
        /// Links to exists return values, where one is caused by another
        /// </summary>
        /// <param name="orignal"></param>
        /// <param name="handler"></param>
        public static ReturnValue ErrorHandlerFail(ReturnValue orignal, ReturnValue handler, string context ="")
        {
            handler.Inner = orignal;
            var fail = FailResult(string.Format("Error handler cascade failure: {0}",context));
            fail.Inner = handler;
            return fail;
        }

        public override string ToString()
        {
            var desc = Error == null
                ? Context
                : string.Format("Context: {0}.   Exception: {1}\r\nStack Trace:\r\n{2}", Context, Error.Message,
                    Error.StackTrace);
            if (Inner != null)
            {
                return string.Format("{0}:\r\n\t{1}", desc, Inner);
            }
            return desc;
        }

        /// <summary>
        /// creates an error chain and returns failure.
        /// </summary>
        /// <param name="inner">ReturnValue containing error information that is the cause of this failure</param>
        /// <returns></returns>
        public static ReturnValue Cascade(ReturnValue inner, string context = "")
        {
            var fail = FailResult(string.IsNullOrEmpty(context) ? "Cascade failure" : context);
            fail.Inner = inner;
            return fail;
        }
        
       

        /// <summary>
        /// throws AbortException if the call fails.  This should only be used inside a Wrap() block.
        /// </summary>
        public void AbortOnFail()
        {
            if (Success) return;

            throw new AbortException(this);
        }

        /// <summary>
        /// ends a Wrap block early due to failre
        /// </summary>
        /// <param name="context"></param>
        public static void Abort(string context)
        {
            FailResult(context).AbortOnFail();
        }

        public static void Abort(ReturnValue getResult)
        {
            throw new AbortException(getResult);
        }


       
    }

    /// <summary>
    /// encapsulates a function result, and supports multi-valued returns for error conditions.
    /// </summary>
    public class ReturnValue<T> : ReturnValue
    {
        public T Value { get; private set; }

        public ReturnValue()
        {
        }

        public ReturnValue(T returnValue)
        {
            Success = true;
            Value = returnValue;

        }

        public ReturnValue(bool success, string context = "", Exception exception = null)
        {
            Success = success;
            Context = context;
            Error = exception;
        }

        public static ReturnValue<T> SuccessResult(T value, string context = null)
        {
            return new ReturnValue<T>
            {
                Success = true,
                Value = value,
                Context = context
            };
        }

        public static ReturnValue<T> FailResult(string context = null, Exception exception = null)
        {
            return new ReturnValue<T>
            {
                Success = false,
                Context = context,
                Error = exception,
            };
        }

        public static ReturnValue<T> Cascade(ReturnValue inner, string context = "")
        {
            //TODO: refactor so that this method does not have duplicate code compared to ReturnValue.Cascase().
            var fail = FailResult(string.IsNullOrEmpty(context) ? "Cascade failure" : context);
            fail.Inner = inner;
            return fail;
        }


        public static ReturnValue<T> Wrap(Func<T> func, string context = "")
        {
            try
            {
                T ret = func();
                var r = SuccessResult(ret, context);
                return r;
            }
            catch (AbortException abort)
            {

                return FailResult(string.Format("FAIL {0} -> {1}", context, abort.Caller));
            }
            catch (Exception ex)
            {
                // log it.
                var r = FailResult(context == "" ? "wrapped action failed" : context, ex);
                return r;
            }
        }

        /// <summary>
        /// throws AbortException if the call fails.  This should only be used inside a Wrap() block.
        /// </summary>
        public new T AbortOnFail()
        {
            if (Success) return Value;

          
            throw new AbortException(this);
       
        }
    }



    public class ReturnValue<TValue, TCode> : ReturnValue<TValue>
    {
        public TCode ErrorCode { get; protected set; }

        public ReturnValue(TValue value)
            : base(value)
        {

        }

        public ReturnValue(TCode code, string context = "", Exception exception = null)
            : base(false, context, exception)
        {
            ErrorCode = code;
        }

        public static ReturnValue<TValue, TCode> FailResult(TCode errorCode, string context = "", Exception exception = null)
        {
            return new ReturnValue<TValue, TCode>(errorCode, context, exception);
        }

       


    }

     /// <summary>
    /// signals to a Wrap()ed function that the function encountered a fatal error needs to early exit.
    /// Pass the orginal Exception as the Inner parameter (if it exists)
    /// </summary>
    [Serializable]
    public class AbortException : Exception
    {
        public ReturnValue Caller { get; private set; }

        public AbortException()
        {
        }

        public AbortException(ReturnValue caller)
            : this()
        {
            Caller = caller;

        }
        public AbortException(string message)
            : base(message)
        {
        }

        public AbortException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AbortException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }


   
}
