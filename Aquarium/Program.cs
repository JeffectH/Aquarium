using System;
using System.Collections.Generic;

namespace Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium(7,
       new List<Fish>()
                    {
                    new Fish("Карп", 10),
                    new Fish("Сом", 7),
                    new Fish("Пиранья", 15),
                    new Fish("Петушки", 5),
                    new Fish("Осетр", 15),
                    });

            aquarium.Work();
        }
    }

    class Aquarium
    {
        private int _amountFish;
        private List<Fish> _fish = new List<Fish>();

        public Aquarium(int amountFish, List<Fish> fish)
        {
            _amountFish = amountFish;
            _fish = fish;
        }

        public void Work()
        {
            const int CommandAddFish = 1;
            const int CommandPullFishOut = 2;
            const int CommandNextIteration = 3;

            int positionLeftListFish = 0;
            int positionLeftListTop = 1;

            while (_fish.Count > 0)
            {
                Console.WriteLine($"Аквариум (вместимость - {_amountFish}, сейчас в аквариуме - {_fish.Count})");
                Console.SetCursorPosition(positionLeftListFish, positionLeftListTop);
                ShowListFish();

                Console.WriteLine($"\nВыберите комнду ({CommandAddFish} - добавить новую рыбку, {CommandPullFishOut} - убрать рыбку из аквариума, {CommandNextIteration} - следующая итерация)");
                Console.Write("Команда:");

                if (int.TryParse(Console.ReadLine(), out int inputUser))
                {
                    switch (inputUser)
                    {
                        case CommandAddFish:
                            AddFish();
                            break;

                        case CommandPullFishOut:
                            PullFishOut();
                            break;

                        case CommandNextIteration:
                            PerformNextIteration();
                            break;

                        default:
                            Console.WriteLine("Такой команды не существует!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод данных!");
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Рыбок в аквариуме больше нет!");
        }

        private void AddFish()
        {
            string type;
            bool isWork = true;

            if (_fish.Count == _amountFish)
            {
                Console.WriteLine("Аквариум заполнен. Больше добавить рыб нельзя!");
                return;
            }

            Console.Write("Введите вид рыбы:");
            type = Console.ReadLine();

            Console.Write("Введите продолжительность жизни рыбы:");

            while (isWork)
            {
                if (int.TryParse(Console.ReadLine(), out int lifeExpectancy))
                {
                    _fish.Add(new Fish(type, lifeExpectancy));
                    Console.WriteLine("Рыба успешно добавлена!");
                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Введите заново!");
                    Console.Write("Введите продолжительность жизни рыбы:");
                }
            }
        }

        private void PullFishOut()
        {
            bool isWork = true;

            Console.Write("Введите номер рыбки, которую хотите убрать:");

            while (isWork)
            {
                if (int.TryParse(Console.ReadLine(), out int fishNumber))
                {
                    if (fishNumber >= 1 && fishNumber <= _fish.Count)
                    {
                        _fish.RemoveAt(fishNumber - 1);
                        Console.WriteLine("Рыба успешно убрана из аквариума!");
                        isWork = false;
                    }
                    else
                    {
                        Console.WriteLine("Введите заново!");
                        Console.Write("Введите номер рыбки, которую хотите убрать:");
                    }
                }
                else
                {
                    Console.WriteLine("Введите заново!");
                    Console.Write("Введите номер рыбки, которую хотите убрать:");
                }
            }
        }

        private void PerformNextIteration()
        {
            foreach (Fish fish in _fish)
            {
                fish.ReducingLives();
            }
        }

        private void ShowListFish()
        {
            int offsetType;
            int offsetLifeExpectancy;
            int maxLengthType = 10;
            int maxLengthLifeExpectancy = 3;

            for (int i = 0; i < _fish.Count; i++)
            {
                offsetType = maxLengthType - _fish[i].Type.ToString().Length;
                offsetLifeExpectancy = maxLengthLifeExpectancy - _fish[i].LifeExpectancy.ToString().Length;

                Console.WriteLine($"{i + 1}. вид:{_fish[i].Type} {new string(' ', offsetType)} осталось прожить {_fish[i].LifeExpectancy} {new string(' ', offsetLifeExpectancy)} состояние:{_fish[i].Condition}");
            }
        }
    }

    class Fish
    {
        public Fish(string type, int lifeExpectancy)
        {
            Type = type;
            LifeExpectancy = lifeExpectancy;
        }

        public string Type { get; private set; }
        public int LifeExpectancy { get; private set; }

        public bool IsCondition => LifeExpectancy > 0 ? true : false;
        public string Condition => IsCondition ? "Жива" : "Мертва";

        public void ReducingLives()
        {
            if (LifeExpectancy > 0)
            {
                LifeExpectancy--;
            }
        }
    }
}