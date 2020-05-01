
<template>
    <div>
        <div>
            
            <h4>Welcome to funwizard!</h4>

            <spinner ref='spinner' size='md' text='Authenticating...' />

            <div id="app">
            <div id="login">
                <div id="description">
                <h1>Login to start</h1>
                <p>Please enter your credentials to login.</p>
                </div> 
                <div id="form" style="width:370px">
                <form   @submit.prevent="login">
                    <label for="email">Email</label>
                    <input type="text" id="email" v-model="uid" placeholder="me@example.com" autocomplete="off">

                    <label for="password">Password</label>&nbsp;
                    <i class="fas" :class="[passwordIcon]" @click="hidePassword = !hidePassword"></i>
                    <input :type="passwordType" id="password" v-model="password" placeholder="**********">

                    <button style="color:black" type="submit">Log in</button>
                </form> <hr/>
                <a v-on:click="logInWithFacebook">
                    <img src="../style/pictures/facebook-login.png" alt="Login with your facebook account">
                </a>  
                

              

 </div>  
  </div>


</div>
  
        </div>

        <hr/>
        <hr/>
                
                <div class='col-md-4 col-md-offset-2 col-sm-6'>
                    <transition name='fade'>
                        <div v-if='showError' class='alert alert-danger' role='alert'>
                            <div v-if='validationErrors.length > 0'>
                            <ul  v-for='(err, index) in validationErrors' :key='index'>
                                <li>{{ err }}</li>
                            </ul>
                            </div>
                            {{ error }}
                        </div>
                    </transition>
                  
                    <div>
                       
           
        </div>  
               

        </div>
    </div>
</template>
<script src="https://unpkg.com/vue-facebook-login-component/dist/vueFacebookLoginComponent.umd.min.js"></script>
 
<script lang='ts'>


    import Vue from 'vue'
    import Spinner from '@/components/ui/VsSpinner.vue' 
    import store from '@/store.ts'
    import axios from 'axios'  
    import { LOG_IN, LOG_OUT, LOG_IN_HARDCODED } from '@/vuex/action-types'
    declare const window: any;


    export default Vue.extend({
        async created () {

                 //load facebookSDK
                await this.loadFacebookSDK(document, "script", "facebook-jssdk");

                //initialise
                await this.initFacebook();
        },
        data () {

            return {
                uid: '',
                pwd: '',
                passwordFieldType : 'password',
                validationErrors: [] as string[],
                error: '',         
                name: '',
                email: '',
                personalID: '',
                 FB: {},
                model: {},
                scope: {},
                failed: false,
                isBusy : false,
                hidePassword: true,
                ress : Promise,
                loading:false

            }

        },      
        computed: { showError (): boolean { return this.error !== '' } },
        methods: {
          passwordType() {
                return this.hidePassword ? 'password' : 'text'
          },
            passwordIcon() {
                return this.hidePassword ? 'fa-eye' : 'fa-eye-slash'
            },
            async SetStates(ress){

            const name = ress.name;
            const email =ress.email;
            const id=ress.id;
            this.$store.state.userName= name;
            this.$store.state.userEmail = email;
            this.$store.state.userId = id;                
            this.$store.state.user= ress; 

            },
            async DofacebookLogin(accessToken : string){
            
              try{
                //connect to back-end (hardcoded)
                const baseUrl = 'http://localhost:5003';
          
                const response  = await axios.get(
                  `${baseUrl}/facebook?token=${accessToken}`);
                    this.ress = response.data; 

                //log the response
                console.log(this.ress) ;

                //state management
                this.SetStates(this.ress); 

                //commit the mutation
                this.$store.commit('SET_USER', this.ress);

                //redirect to secure page
                this.$router.push('/Welcome')

              }catch(error){  
                //display network error
                this.error = error;
              }

            },
            switchVisibility() {
                this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password';
             },
            login () {  

                //regular login 

                this.error = ''
                if (!this.validate()) return 
                this.$store.dispatch(LOG_IN_HARDCODED, {uid: this.uid, pwd: this.pwd})
                .then(() =>  this.$router.push('/About'))
                .catch(err => {  
                    this.error = err
                })
            }, 
            validate () { 
                //validation is any 
                return true
            },       
           
            async logInWithFacebook() {

                //to access this inside promise
                var thisClass = this;

                //reset error
                this.error = '';

                //Login
                window.FB.login(function(response : any) {

                    //check error?
                    var hasError = Object.prototype.hasOwnProperty.call(response, "error");
 
 
                    if (!hasError && response.authResponse) {

                        let Token = response.authResponse.accessToken;
 
                        thisClass.DofacebookLogin(Token);
                      
                        return true;

                    }else{                      

                         thisClass.error = "Facebook Authentication failed";
                    }
                });


                return false;

                },
                async initFacebook() {
                window.fbAsyncInit = function() {
                    window.FB.init({
                    appId: "2291259327844895", //SET your app ID here
                    cookie: true,  
                    version: "v13.0"
                    });
                };
                },

                async loadFacebookSDK(d : any, s : any, id : any) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) {
                    return;
                }
                js = d.createElement(s);
                js.id = id;
                js.src = "https://connect.facebook.net/en_US/sdk.js";
                fjs.parentNode.insertBefore(js, fjs);
                },
            
               components: {   Spinner }
        }
    })

</script>


<style>

@import "https://use.fontawesome.com/releases/v5.0.6/css/all.css";
 

* {
  box-sizing: border-box;
  font-family: 'Nunito', sans-serif;
}

html,
body {
  height: 100%;
  margin: 0;
  padding: 0;
  width: 100%;
}

div#app {
  width: 100%;
  height: 100%;
}

div#app div#login {
  align-items: center;
  background-color: #e2e2e5;
  display: flex;
  justify-content: center;
  width: 100%;
  height: 100%;
}

div#app div#login div#description {
  background-color: #ffffff;
  width: 280px;
  padding: 35px;
}

div#app div#login div#description h1,
div#app div#login div#description p {
  margin: 0;
}

div#app div#login div#description p {
  font-size: 0.8em;
  color: #95a5a6;
  margin-top: 10px;
}

div#app div#login div#form {
  background-color: #34495e;
  border-radius: 5px;
  box-shadow: 0px 0px 30px 0px #666;
  color: #ecf0f1;
  width: 260px;
  padding: 35px;
}

div#app div#login div#form label,
div#app div#login div#form input {
  outline: none;
  width: 100%;
}

div#app div#login div#form label {
  color: #95a5a6;
  font-size: 0.8em;
}

div#app div#login div#form input {
  background-color: transparent;
  border: none;
  color: #ecf0f1;
  font-size: 1em;
  margin-bottom: 20px;
}

div#app div#login div#form ::placeholder {
  color: #ecf0f1;
  opacity: 1;
}

div#app div#login div#form button {
  background-color: #ffffff;
  cursor: pointer;
  border: none;
  padding: 10px;
  transition: background-color 0.2s ease-in-out;
  width: 100%;
}

div#app div#login div#form button:hover {
  background-color: #eeeeee;
}

@media screen and (max-width: 600px) {
  div#app div#login {
    align-items: unset;
    background-color: unset;
    display: unset;
    justify-content: unset;
  }

  div#app div#login div#description {
    margin: 0 auto;
    max-width: 350px;
    width: 100%;
  }

  div#app div#login div#form {
    border-radius: unset;
    box-shadow: unset;
    width: 100%;
  }

  div#app div#login div#form form {
    margin: 0 auto;
    max-width: 360px;
    width: 100%;
  }
}
</style>