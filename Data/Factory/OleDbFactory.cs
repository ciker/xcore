//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Factory\AccessFactory.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
namespace System.Data {
    /// <summary>
    /// access ���ݹ�������ȡ Connection, Command, DataAdapter
    /// </summary>
    public partial class AccessFactory : DbFactoryBase {
        public override IDbConnection GetConnection( String connectionString ) {
            return new OleDbConnection( connectionString );
        }
        public override IDbCommand GetCommand( String CommandText ) {
            IDbCommand cmd = new OleDbCommand();
            cmd.Connection = cn;
            cmd.CommandText = CommandText;
            setTransaction( cmd );
            return cmd;
        }
        public override IDatabaseDialect GetDialect() {
            return new AccessDialect();
        }
        public override Object SetParameter( IDbCommand cmd, String parameterName, Object parameterValue ) {
            parameterValue = base.processValue( parameterValue );
            parameterName = new AccessDialect().GetParameterAdder( parameterName );
            IDbDataParameter parameter;
            if (parameterValue is DateTime) {
                parameter = new OleDbParameter( parameterName, parameterValue.ToString() );
            }
            else {
                parameter = new OleDbParameter( parameterName, parameterValue );
            }
            cmd.Parameters.Add( parameter );
            return parameterValue;
        }
        public override DbDataAdapter GetAdapter() {
            return new OleDbDataAdapter( (OleDbCommand)cmd );
        }
        public override DbDataAdapter GetAdapter( String CommandText ) {
            return new OleDbDataAdapter( (OleDbCommand)GetCommand( CommandText ) );
        }
    }
}