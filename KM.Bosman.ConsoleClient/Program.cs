using KM.Bosman.FakeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KM.Bosman.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // ObserverTest();

            // TimerTest();

            SpeedFaker speedFaker = new SpeedFaker();

            var source = new ReplaySubject<float>(3);

            source.OnNext(100);
            source.OnNext(150);
            source.OnNext(100);
            source.OnNext(150);
            source.OnNext(50);
            source.OnNext(650);
            source.OnNext(700);
            source.OnNext(850);
            source.OnNext(100);
            source.OnNext(150);

            source.Subscribe(item => Console.WriteLine($"S1 #{Thread.CurrentThread.ManagedThreadId} {item}"));
            source.Subscribe(item => Console.WriteLine($"S2 #{Thread.CurrentThread.ManagedThreadId} {item}"));

            source.OnNext(250);



            Console.WriteLine("Press any key to exit.");

            Console.ReadLine();


        }

        private static void TimerTest()
        {
            SpeedFaker speedFaker = new SpeedFaker();


            IObservable<float> source = Observable.Interval(TimeSpan.FromSeconds(1))
              .Select(_ => speedFaker.Generate().Value);

            // .Select(_ => 10f);


            var filtered = source
                .Where(speed => speed > 10);


            filtered
                .Subscribe(speed =>
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} ALERT {speed}");
                    Console.ResetColor();
                }
                );



            source
            //    .DistinctUntilChanged()
                .Subscribe(item => Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Speed {item}"));
        }

        private static void ObserverTest()
        {
            MyObserver observer = new MyObserver();

            MyObservable source = new MyObservable();
            IDisposable subsrciption = source.Subscribe(observer);

            subsrciption.Dispose();

            try
            {
                using (var subsrciption2 = source.Subscribe(observer))
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {

            }
        }
    }

    public class MyObservable : IObservable<float>
    {
        private IObserver<float> observer;

        public IDisposable Subscribe(IObserver<float> observer)
        {
            this.observer = observer;

            this.observer.OnNext(100);
            this.observer.OnNext(70);
            this.observer.OnNext(160);

            this.observer.OnCompleted();

            return null;
        }
    }

    public class MyObserver : IObserver<float>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(float value)
        {
            Console.WriteLine(value);
        }
    }
}
