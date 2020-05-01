<template>
 
       <div>
          <h3>This is a private area</h3>
          <hr/>
          <div>
            <strong>Welcome {{Name}}!</strong>
            <hr/>
            <strong>Your facebookid:{{id}}</strong>
             <hr/>
            <strong>Email: {{email}}</strong>
             <hr/>
          </div>
         <b>Lets get started!</b>
      </div> 
 
</template>
 

 <script lang='ts'>

    import Vue from 'vue' 

    export default Vue.extend({
        data () {

            return {
                Name : this.$store.state.userName, //state.userName,
                id : this.$store.state.userId,
                email : this.$store.state.userEmail
            }

        }
    })

</script>