import React ,{Component}from 'react';
import ReactDom from 'react-dom';
class Sevice extends Component
{
    render()
    {
        console.log(this.props.booking.Time);
        return(
              <tr>
            <td>{this.props.booking.Time}</td>
            <td><input type='button' value='Book'/></td>
            </tr>
            );
    }
   

}

export default Sevice;