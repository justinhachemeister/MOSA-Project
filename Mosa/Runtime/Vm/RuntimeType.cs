/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (<mailto:sharpos@michaelruck.de>)
 */

using System;
using System.Collections.Generic;
using System.Text;

using Mosa.Runtime.Loader;
using Mosa.Runtime.Metadata;
using Mosa.Runtime.Metadata.Tables;

namespace Mosa.Runtime.Vm
{
    /// <summary>
    /// 
    /// </summary>
	public enum RuntimeTypeFlags {

        /// <summary>
        /// 
        /// </summary>
		Loaded = 0x01,

		/// <summary>
		/// Type is a generic type.
		/// </summary>
		HasGenericParams = 0x40,

		/// <summary>
		/// Type is a generic specialization.
		/// </summary>
		HasGenericArgs = 0x80
	}

    /// <summary>
    /// 
    /// </summary>
    public class RuntimeType : IEquatable<RuntimeType>
    {
        #region Data members

        /// <summary>
		/// Holds the type index of the base class.
		/// </summary>
		/// <remarks>
		/// The value is incremented by one in order to differentiate uninitialized values from valid values. Invalid
		/// values are zero and below.
		/// </remarks>
		private int _extends;

        /// <summary>
        /// Holds the type flag.
        /// </summary>
        private TypeAttributes _flags;

        /// <summary>
        /// The metadata module, which owns this type.
        /// </summary>
        private IMetadataModule _module;

        /// <summary>
        /// Holds the (cached) name of the type.
        /// </summary>
        public string _name;

        /// <summary>
        /// The name index of the defined type.
        /// </summary>
        private TokenTypes _nameIdx;

        /// <summary>
        /// Holds the (cached) namespace of the type.
        /// </summary>
        public string _namespace;

        /// <summary>
        /// The namespace index of the defined type.
        /// </summary>
        private TokenTypes _namespaceIdx;

        /// <summary>
        /// Holds the calculated native size of the type.
        /// </summary>
        private uint _nativeSize;

        /// <summary>
        /// 
        /// </summary>
		private uint _packing;

        // <summary>
        // Holds generic parameters or types.
        // </summary>
        //private object[] _generics;

        /// <summary>
        /// Methods of the type.
        /// </summary>
        private ReadOnlyRuntimeMethodListView _methods;

        /// <summary>
        /// Holds the fields of this type.
        /// </summary>
        private ReadOnlyRuntimeFieldListView _fields;

        /// <summary>
        /// Holds the token of the type.
        /// </summary>
        private TokenTypes _token;

        #endregion // Data members

        /// <summary>
        /// 
        /// </summary>
        public RuntimeTypeFlags Flags;
        /// <summary>
        /// 
        /// </summary>
		public TypeDefRow _typeDef;
        /// <summary>
        /// 
        /// </summary>
        public GenericArgument[] _arguments;

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeType"/> class.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="typeDefRow">The type def row.</param>
        /// <param name="maxField">The max field.</param>
        /// <param name="maxMethod">The max method.</param>
        /// <param name="packing">The packing.</param>
        /// <param name="size">The size.</param>
        public RuntimeType(IMetadataModule module, ref TypeDefRow typeDefRow, TokenTypes maxField, TokenTypes maxMethod, uint packing, uint size)
        {
            int members;
            _token = 0;
            _module = module;
            _extends = RuntimeBase.Instance.TypeLoader.FindTypeIndexFromToken(module, typeDefRow.Extends);
            _flags = typeDefRow.Flags;
            _nameIdx = typeDefRow.TypeNameIdx;
            _namespaceIdx = typeDefRow.TypeNamespaceIdx;
            _nativeSize = size;
            _packing = packing;

            // Load all fields of the type
            members = maxField - typeDefRow.FieldList + 1;
            if (0 < members)
            {
                int i = (int)(typeDefRow.FieldList & TokenTypes.RowIndexMask) - 1 + RuntimeBase.Instance.TypeLoader.GetModuleOffset(module).FieldOffset;
                _fields = new ReadOnlyRuntimeFieldListView(i, members);
            }
            else
            {
                _fields = ReadOnlyRuntimeFieldListView.Empty;
            }

            // Load all methods of the type
            members = maxMethod - typeDefRow.MethodList + 1;
            if (0 < members)
            {
                int i = (int)(typeDefRow.MethodList & TokenTypes.RowIndexMask) - 1 + RuntimeBase.Instance.TypeLoader.GetModuleOffset(module).MethodOffset;
                _methods = new ReadOnlyRuntimeMethodListView(i, members);
            }
            else
            {
                _methods = ReadOnlyRuntimeMethodListView.Empty;
            }
        }

        #endregion // Construction

        #region Properties

        /// <summary>
        /// Determines if the type has generic arguments.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is generic; otherwise, <c>false</c>.
        /// </value>
        public bool IsGeneric
        {
            get { return (null != _arguments && 0 != _arguments.Length); }
        }

        /// <summary>
        /// Retrieves the base class of the represented type.
        /// </summary>
        /// <value>The extends.</value>
		public int Extends 
        {
			get { return (_extends - 1); }
			set {
				if (value < 0)
					throw new ArgumentException(@"Invalid index.");

				_extends = value + 1;
			}
        }

        /// <summary>
        /// Returns the fields of the type.
        /// </summary>
        /// <value>The fields.</value>
        public IList<RuntimeField> Fields
        {
            get { return _fields; }
        }

        /// <summary>
        /// Returns the methods of the type.
        /// </summary>
        /// <value>The methods.</value>
        public IList<RuntimeMethod> Methods
        {
            get { return _methods; }
        }

        /// <summary>
        /// Retrieves the name of the represented type.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                if (null == _name)
                    _module.Metadata.Read(_nameIdx, out _name);

                return _name;
            }
        }

        /// <summary>
        /// Retrieves the namespace of the represented type.
        /// </summary>
        /// <value>The namespace.</value>
        public string Namespace
        {
            get
            {
                if (null == _namespace)
                    _module.Metadata.Read(_namespaceIdx, out _namespace);

                return _namespace;
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>The token.</value>
        public TokenTypes Token
        {
            get { return _token; }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Sets generic parameters on this method.
        /// </summary>
        /// <param name="gprs">A list of generic parameters to set on the method.</param>
        public void SetGenericParameter(List<GenericParamRow> gprs)
        {
            // TODO: Implement this method
            _arguments = new GenericArgument[gprs.Count];
        }

        #endregion // Methods

        #region IEquatable<RuntimeType> Members

        /// <summary>
        /// Gibt an, ob das aktuelle Objekt gleich einem anderen Objekt des gleichen Typs ist.
        /// </summary>
        /// <param name="other">Ein Objekt, das mit diesem Objekt verglichen werden soll.</param>
        /// <returns>
        /// true, wenn das aktuelle Objekt gleich dem <paramref name="other"/>-Parameter ist, andernfalls false.
        /// </returns>
        public bool Equals(RuntimeType other)
        {
            return (_module == other._module && _nameIdx == other._nameIdx && _namespaceIdx == other._namespaceIdx && _extends == other._extends);
        }

        #endregion // IEquatable<RuntimeType> Members

        #region Object Overrides

        /// <summary>
        /// Gibt einen <see cref="T:System.String"/> zur�ck, der den aktuellen <see cref="T:System.Object"/> darstellt.
        /// </summary>
        /// <returns>
        /// Ein <see cref="T:System.String"/>, der den aktuellen <see cref="T:System.Object"/> darstellt.
        /// </returns>
        public override string ToString()
        {
            return String.Format("{0}.{1}", this.Namespace, this.Name);
        }

        #endregion // Object Overrides
    }
}
