namespace System.Security.Permissions
{
	public class SecurityPermissionAttribute : Attribute
	{
		public SecurityPermissionAttribute(SecurityAction action)
		{
		}

		public bool SkipVerification
		{
			get
			{
				return true;
			}
			set
			{

			}
		}
	}
}