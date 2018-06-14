import React ,{Component}from 'react';
import ReactDom from 'react-dom';
import $ from 'jquery';

import ServiceList from './ServiceList';

class CarWash extends Component
{
    constructor()
    {
        super();
        this.CloseMessage= this.CloseMessage.bind(this);
        this.state={bookings:[],Alertmessage:""};
    }
    componentDidMount()
    {
        this.GetBookings();
    }

    HandleBooking(param)
    {
        console.log(param);
        $.ajax(
            {
                url: "/Home/UpdateBooking",
                type:'POST',
                data:param,
                success: function(data){
                    this.setState({bookings:data,Alertmessage:"Updated Successfully"})
                   
                }.bind(this),
                error: function(xhr, textStatus, errorThrown){
                    console.log(errorThrown);
                }
            }
            );
    }

    CloseMessage(e)
    {
        e.preventDefault();
        this.setState({Alertmessage:""});
    }

    GetBookings()
    {
        $.ajax(
            {
                url: "/Home/GetBookings",
                datatype:'json',
                type:'GET',
                success: function(data){
                    this.setState({bookings:data})
                }.bind(this),
                error: function(xhr, textStatus, errorThrown){
                    console.log(errorThrown);
                }
            }
            );
    }
    render()
    {
        const Customstyle= this.state.Alertmessage==''?{display:'none'}:{};

        return (
            <div>
        <ServiceList servicelist={this.state.bookings} BookVacancy= {this.HandleBooking.bind(this)}/>     
            
            <div>{this.state.Alertmessage}
            <a href="#" onClick={(e) => this.CloseMessage(e)} style={Customstyle}>X</a>
                </div>
            </div>
    );
        }
   

}

export default CarWash;