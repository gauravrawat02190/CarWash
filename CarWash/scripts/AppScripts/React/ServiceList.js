import React ,{Component}from 'react';
import ReactDom from 'react-dom';
import $ from 'jquery';
import EditRequest from './EditRequest';

class ServiceList extends Component
{
    constructor(props)
    {
        super(props);       
        this.state={bookings:this.props.servicelist,timeslot:'', EditStyle:{display:'none'},ActiveId:0}
    }
    BookVacancy(param)
    {
        this.props.BookVacancy(param);
    }
   
    CancelRequest()
    {
        this.setState({EditStyle:{display:'none'},timeslot:'',ActiveId:0});
    }

    ShowEditRequest(params)
    {
        if (params.ShowRequest)
        {
            this.setState({EditStyle:null,timeslot:params.Time,ActiveId:params.ActiveId});
        }

    }
   

    render()
    {
        let serviceitems;
        if(this.props.servicelist)
        {

            serviceitems= this.props.servicelist.map(p => 
            
                <tr key={p.id}>
           <td align="center">{p.Time}</td>
            <td align="center"><input type='button' disabled={!p.IsVacant} value='Book' onClick={this.BookVacancy.bind(this,p)}/> 
    <span style={{ visibility: this.state.ActiveId != p.id? 'visible': 'hidden'}}>
         <EditRequest Show={p} ShowEditRequest={this.ShowEditRequest.bind(this)}/> 
        </span>       
            </td>
            </tr>               
                );

        }
       
    return (

 <table width="50%" align='center'>
<tbody>
 <tr><th> Time Slot </th>
<th>Action</th></tr>
    {serviceitems}

    <tr>
    <td>
    <div style={this.state.EditStyle}>
Your Slot <input type='text' readOnly='true' value={this.state.timeslot} />
<input type='button' value='Submit' disabled/>
<input type='button' value='Cancel' onClick={this.CancelRequest.bind(this)}/>
        </div>
    </td>
    </tr>
</tbody>
</table>

);
    }

               
}              
    export default ServiceList;
