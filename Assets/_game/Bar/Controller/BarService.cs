using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.BarStatus;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Bar.View;
using Assets._game.Hint.Controller;
using Assets._game.Shift.Controller;
using Assets._game.Sound.Controller;
using Assets._game.Sound.EnumInterface;
using Assets._game.TestingScript;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.Controller {
    public class BarService : IBarService, IInitializable {

        [Inject] IHintService hintService;

        DeskManagerView deskManagerView;
        IEconomyService economyService;
        WaitingLineService waitingLineService;
        ISeatService seatService;
        AlcoholCatalog alcoholCatalogSO;
        ISFXService sfxService;

        Dictionary<AlcoholType, int> alcohols = new();


        public event Action<float, Action> OnNpcRequestBar;


        Vibe vibe = new();
        ChaosStatus chaosStatus = new();
        public Vibe GetVibe() => vibe;

        public void AddVibe( int count ) {
            vibe.AddVibe(count);
            deskManagerView.UpdateVibe(GetVibe().vibe);
        }

        public void ReduceVibe( int count ) {
            vibe.ReduceVibe(count);
            deskManagerView.UpdateVibe(GetVibe().vibe);
        }
        public ChaosStatus GetChaosStatus() => chaosStatus;
        public void AddChaos( float amt ) {
            chaosStatus.AddChaos(amt);
            deskManagerView.UpdateChaosScale(chaosStatus.chaosScale);
        }
        public void ReduceChaos( float amt ) {
            chaosStatus.ReduceChaos(amt);
            deskManagerView.UpdateChaosScale(chaosStatus.chaosScale);
        }


        [Inject]
        void Construct( DeskManagerView deskManagerView, IEconomyService economyService,
            [Inject(Id = "Bar")] WaitingLineService waitingLine,
            ISeatService seatService,
            AlcoholCatalog alcoholCatalogSO,
            ISFXService sfxService ) {
            this.deskManagerView = deskManagerView;
            this.economyService = economyService;
            this.waitingLineService = waitingLine;
            this.seatService = seatService;
            this.alcoholCatalogSO = alcoholCatalogSO;
            this.sfxService = sfxService;
        }

        public void Initialize() {
            InitAlchoholData();

        }

        private void InitAlchoholData() {

            AlcoholSO[] newAlcohols = Resources.LoadAll<AlcoholSO>("Bar/Alchohol/DrinkSO/Alcohol");
            Array.Sort(newAlcohols, ( a, b ) => a.BuyCost.CompareTo(b.BuyCost));

            foreach ( AlcoholSO alcohol in newAlcohols ) {
                alcohols.Add(alcohol.AlcoholType, 0);
            }
        }

        public Dictionary<AlcoholType, int> GetAlcoholDictionary() => alcohols;

        public void AddAlcohol( AlcoholType alcoholType, int count ) {

            alcohols[alcoholType] += count;

            hintService.RemoveHint(Hint.Model.HintType.NoAlcohol);
            Debug.Log($"<color=yellow>Added {alcoholType} +{count}</color>");
        }

        public void ReduceAlchohol( AlcoholType alcoholType, int count ) {
            alcohols[alcoholType] -= count;

            if ( alcohols.Count < 1 )
                hintService.AddHint(Hint.Model.HintType.NoAlcohol);

            Debug.Log($"<color=yellow>Reduce {alcoholType} -{count}</color>");
        }

        public void RequestDrink( AlcoholOrder alcoholOrder, Action onOrderReady ) {
            var so = alcoholCatalogSO.Get(alcoholOrder.alcoholType);

            Debug.Log($"Order {alcoholOrder.alcoholType}");


            OnNpcRequestBar?.Invoke(so.PrepareTime, () => {
                onOrderReady?.Invoke();
                sfxService.Play(SFXType.CashIn);
                economyService.SellAlchohol(alcoholOrder.alcoholType);
            });


        }






        //public IEnumerator RequestOrder( NPCScript NPCScript, Order order, Action onOrderReady ) {
        //    switch ( order ) {
        //        case FoodOrder:
        //            yield return MakeFood(NPCScript, order);
        //            break;

        //        case DrinkOrder:
        //            yield return MakeDrink(NPCScript, order);
        //            break;

        //        default:
        //            yield return MakeFood(NPCScript, null);
        //            break;
        //    }

        //    onOrderReady?.Invoke();
        //}






        ////TOOD: it should read the SO instead of hardcode
        //public IEnumerator MakeFood( NPCScript NPCScript, Order order ) {

        //    yield return new WaitForSeconds(10);

        //    Debug.Log("food ready");


        //}

        //public IEnumerator MakeDrink( NPCScript NPCScript, Order order ) {
        //    yield return new WaitForSeconds(5);

        //    Debug.Log("drink ready");


        //}

    }


}