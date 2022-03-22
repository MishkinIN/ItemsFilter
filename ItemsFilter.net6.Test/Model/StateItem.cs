using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsFilter.net6.Test.Model {
    internal class StateItem {
        private static Lazy<IList> lzAll = new(() => {
            List<StateItem> enumNames = new();
            enumNames.AddRange(Enum.GetValues<StateEnum>().Select(v => new StateItem(v)));
            return enumNames;
        });
        internal static IList All { get => lzAll.Value; }
        public StateItem():this(default) {
        }
        public StateItem(StateEnum state) {
            State = state;
            Id= (int)state;
            Box = (object)state;
        }
        public int? Id { get; set; }
        public int StateId { get { return (int)State; } }
        public StateEnum State { get; set; }
        public string StateText { get { return State.ToString();} }
        public object Box { get; set; }
    }
}
