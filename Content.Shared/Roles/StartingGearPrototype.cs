using Content.Shared.Preferences;
using Robust.Shared.Prototypes;
using Content.Shared.Humanoid;

namespace Content.Shared.Roles
{
    [Prototype("startingGear")]
    public sealed partial class StartingGearPrototype : IPrototype
    {
        [DataField]
        public Dictionary<string, EntProtoId> Equipment = new();

        /// <summary>
        /// if empty, there is no skirt override - instead the uniform provided in equipment is added.
        /// </summary>
        [DataField]
        public EntProtoId? InnerClothingSkirt;

        [DataField]
        public EntProtoId? Satchel;

        [DataField]
        public EntProtoId? Duffelbag;

        [DataField]
        public List<EntProtoId> Inhand = new(0);

        // Может пора подписывать что это IanStation
        [DataField("underwearb")]
        private string _underwearb = string.Empty;

        [DataField("underweart")]
        private string _underweart = string.Empty;
        // Потом думаю можно

        [ViewVariables]
        [IdDataField]
        public string ID { get; private set; } = string.Empty;

        public string GetGear(string slot, HumanoidCharacterProfile? profile)
        {
            if (profile != null)
            {
                if (slot == "jumpsuit" && profile.Clothing == ClothingPreference.Jumpskirt && !string.IsNullOrEmpty(InnerClothingSkirt))
                    return InnerClothingSkirt;
                if (slot == "back" && profile.Backpack == BackpackPreference.Satchel && !string.IsNullOrEmpty(Satchel))
                    return Satchel;
                if (slot == "back" && profile.Backpack == BackpackPreference.Duffelbag && !string.IsNullOrEmpty(Duffelbag))
                    return Duffelbag;
                // Ian Station start
                if (slot == "underwearb" && profile.Sex == Sex.Female && !string.IsNullOrEmpty(_underwearb))
                    return _underwearb;
                if (slot == "underweart" && profile.Sex == Sex.Female && !string.IsNullOrEmpty(_underweart))
                    return _underweart;
                // Ian Station stop
            }

            return Equipment.TryGetValue(slot, out var equipment) ? equipment : string.Empty;
        }
    }
}
